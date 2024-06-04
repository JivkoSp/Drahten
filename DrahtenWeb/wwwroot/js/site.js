
//This function creates bootstrap 5 card element for document object, returned from SearchService.
//The card element includes form element with hidden input elements that represent data from the document object.
//Arguments:
// - divElementArticleList: html div element to which the card element will be added.
// - element: document object returned from SearchService.
// - cardTitle: name of the card element.
//Returns: void.
function CreateDocumentCard(divElementArticleList, article, articleComments, usersRelatedToArticle) {

    const divElementCardContainer = $("<div>", {
        class: "col-4 mt-4"
    });

    const divElementCard = $("<div>", {
        class: "card h-100"
    });

    const divElementCardBody = $("<div>", {
        class: "card-body"
    });
     
    const hElementCardTitle = $("<h5>", {
        class: "card-title text-center"
    });

    hElementCardTitle.text(article.TopicFullName);

    const pElementCardText = $("<p>", {
        class: "card-text"
    });

    pElementCardText.text(article.PrevTitle);

    const divElementCardFooter = $("<div>", {
        class: "card-footer d-flex justify-content-between align-items-center"
    });

    const smallElementCardFooterInfo = $("<small>", {
        class: "text-muted"
    });

    smallElementCardFooterInfo.text(`Comments: ${articleComments.length} | Views: ${usersRelatedToArticle.length}
    | Likes: ${article.ArticleLikeDtos.length} | Dislikes: ${article.ArticleDislikeDtos.length}`);

    const buttonElementCardFooter = $("<button>", {
        type: "button",
        'data-formId': "card-form-" + article.ArticleId,
        class: "btn btn-primary",
        onclick: "submitCardForm()"
    });

    buttonElementCardFooter.text("Read");

    const formElement = $("<form>", {
        id: "card-form-" + article.ArticleId,
        action: "/Article/ViewArticle",
        class: "shadow rounded",
        method: "post"
    });

    const inputElementArticleId = $("<input>", {
        type: "hidden",
        name: "ArticleId",
        value: article.ArticleId
    });

    const inputElementArticlePrevTitle = $("<input>", {
        type: "hidden",
        name: "PrevTitle",
        value: article.PrevTitle
    });

    const inputElementArticleTitle = $("<input>", {
        type: "hidden",
        name: "Title",
        value: article.Title
    });

    const inputElementArticleData = $("<input>", {
        type: "hidden",
        name: "Content",
        value: article.Content
    });

    const inputElementArticlePublishedDate = $("<input>", {
        type: "hidden",
        name: "PublishingDate",
        value: article.PublishingDate
    });

    const inputElementArticleAuthor = $("<input>", {
        type: "hidden",
        name: "Author",
        value: article.Author
    });

    const inputElementArticleLink = $("<input>", {
        type: "hidden",
        name: "Link",
        value: article.Link
    });

    const inputElementTopicId = $("<input>", {
        type: "hidden",
        name: "TopicId",
        value: article.TopicId
    });

    const inputElementArticleLikes = $("<input>", {
        type: "hidden",
        name: "ArticleLikeDtos",
        value: JSON.stringify(article.ArticleLikeDtos)
    });

    const inputElementArticleDislikes = $("<input>", {
        type: "hidden",
        name: "ArticleDislikeDtos",
        value: JSON.stringify(article.ArticleDislikeDtos)
    });

    const inputElementArticleComments = $("<input>", {
        type: "hidden",
        name: "articleComments",
        value: JSON.stringify(articleComments)
    });

    const inputElementUserArticles = $("<input>", {
        type: "hidden",
        name: "usersRelatedToArticle",
        value: JSON.stringify(usersRelatedToArticle)
    });

    formElement.append(inputElementArticleId);
    formElement.append(inputElementArticlePrevTitle);
    formElement.append(inputElementArticleTitle);
    formElement.append(inputElementArticleData);
    formElement.append(inputElementArticlePublishedDate);
    formElement.append(inputElementArticleAuthor);
    formElement.append(inputElementArticleLink);
    formElement.append(inputElementTopicId);
    formElement.append(inputElementArticleLikes);
    formElement.append(inputElementArticleDislikes);
    formElement.append(inputElementArticleComments);
    formElement.append(inputElementUserArticles);

    divElementCardFooter.append(smallElementCardFooterInfo);
    divElementCardFooter.append(buttonElementCardFooter);
    divElementCardBody.append(hElementCardTitle);
    divElementCardBody.append(pElementCardText);
    divElementCardBody.append(formElement);
    divElementCard.append(divElementCardBody);
    divElementCard.append(divElementCardFooter);
    divElementCardContainer.append(divElementCard);
    divElementArticleList.append(divElementCardContainer);
}


function highlightMatchingText(textOne, textTwo, textTree = null) {

    //Escape special characters in textTwo for safe use in regular expression.
    const escapedTextTwo = textTwo.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// Create a regular expression to find all occurrences of textTwo within one or more sentences in textOne.
    ///
    /// [^.!?]* matches any character except ., ?, and ! zero or more times.
    /// [.!?] matches the end of a sentence.
    /// The gi flags make the regular expression global and case-insensitive (find all matches).
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    const regex = new RegExp(`[^.!?]*${escapedTextTwo}[^.!?]*[.!?]`, 'gi');

    //Replace the matches in textOne with <span>...</span> wrapped matches.
    let highlightedText = textOne.replace(regex, (match) => {
        return `<span style="background-color: yellow">${match}</span>`;
    });

    if (textTree != null) {

        highlightedText = highlightedText.replace(textTree, (match) => {
            return `<span style="background-color: #ff8000">${match}</span>`;
        });
    }

    return highlightedText;
}


// This function creates table element.
// Arguments: tableData - This is object that represents the structure and the data of the table by key-value pairs.
// The key represents column name, the value represents array with value for each column.
// Returns: table element.
function CreateHistoryTable(tableData) {

    if (tableData == null) {
        return null;
    }

    const tableElement = $("<table>", {
        class: "table mb-0"
    });

    const theadElement = $("<thead>", {

    });

    const tbodyElementTableBody = $("<tbody>", {

    });

    const trElementTableHead = $("<tr>", {

    });

    let tableRows = 0;

    for (const key in tableData) {
        if (tableData.hasOwnProperty(key)) {
            console.log(`Key: ${key}, Value: ${tableData[key]}`);

            const thElement = $("<th>", {
                 scope: "col",
                 text: key
            });

            trElementTableHead.append(thElement);

            // Calculating the number of rows for the table.
            tableRows = tableData[key].length;
        }
    }

    for (let i = 0; i < tableRows; i++) {

        const trElementTableRow = $("<tr>");

        for (const key in tableData) {

            if (tableData.hasOwnProperty(key)) {

                const tdElement = $("<td>", {
                    text: tableData[key][i]
                });

                trElementTableRow.append(tdElement);
            }
        }

        tbodyElementTableBody.append(trElementTableRow);
    }

    theadElement.append(trElementTableHead);
    tableElement.append(theadElement);
    tableElement.append(tbodyElementTableBody);

    return tableElement;
}


// This function creates pagination for the history table.
// Arguments: pagination - This is object that contains information for the totalPages, currentPage, startPage and endPage of the table.
//            pageButtonId - This preffix for the ID of each button for the pagination. 
// Returns: <div></div> element that will contain the pagination for the table.
function CreateHistoryTablePagination(pagination, pageButtonIdPreffix) {

    const divElementPaginationContainer = $("<div>", {
        class: "container mt-1"
    });

    const ulElementPagination = $("<ul>", {
        class: "pagination justify-content-end"
    });

    for (var page = pagination.startPage; page <= pagination.endPage; page++) {

        const liElementPageItem = $("<li>", {
            class: `page-item ${page === pagination.currentPage ? "active" : ""}`
        });

        const buttonElementPageItemLink = $("<button>", {
            id: `${pageButtonIdPreffix}-${page}`,
            class: "page-link",
            text: page
        });

        liElementPageItem.append(buttonElementPageItemLink);
        ulElementPagination.append(liElementPageItem);
    }

    if (pagination.currentPage > 1) {

        const liElementPageItemFirst = $("<li>", {
            class: `page-item`
        });

        const buttonElementPageItemLinkFirst = $("<button>", {
            id: `${pageButtonIdPreffix}-1`,
            class: "page-link",
            text: "First"
        });

        liElementPageItemFirst.append(buttonElementPageItemLinkFirst);
        ulElementPagination.prepend(liElementPageItemFirst);
    }

    if (pagination.currentPage < pagination.totalPages) {

        const liElementPageItemLast = $("<li>", {
            class: `page-item`
        });

        const buttonElementPageItemLinkLast = $("<button>", {
            id: `${pageButtonIdPreffix}-${pagination.totalPages}`,
            class: "page-link",
            text: "Last"
        });

        liElementPageItemLast.append(buttonElementPageItemLinkLast);
        ulElementPagination.append(liElementPageItemLast);
    }

    divElementPaginationContainer.append(ulElementPagination);

    return divElementPaginationContainer;
}