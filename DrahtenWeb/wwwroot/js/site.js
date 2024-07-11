
//This function creates bootstrap 5 card element for document object, returned from SearchService.
//The card element includes form element with hidden input elements that represent data from the document object.
//Arguments:
// - divElementArticleList: html div element to which the card element will be added.
// - element: document object returned from SearchService.
// - cardTitle: name of the card element.
//Returns: void.
function CreateDocumentCard(divElementArticleList, article, articleComments, usersRelatedToArticle) {

    const divElementCardContainer = $("<div>", {
        class: "col-12 mt-4 col-md-6 col-xl-4"
    });

    const divElementCard = $("<div>", {
        class: "card-article shadow"
    });

    const divElementCardBody = $("<div>", {
        class: "card-article-body d-flex flex-column justify-content-between p-3"
    });
     
    const hElementCardTitle = $("<h5>", {
        class: "card-article-title text-center mb-3"
    });

    hElementCardTitle.text(article.title);

    const pElementCardText = $("<p>", {
        class: "card-article-text text-muted mb-3"
    });

    pElementCardText.text(article.prevTitle);

    const divElementCardFooter = $("<div>", {
        class: "card-article-footer bg-transparent border-top-0 d-flex justify-content-between align-items-center p-3"
    });

    const smallElementCardFooterInfo = $("<small>", {
        class: "card-article-info text-muted"
    });

    smallElementCardFooterInfo.text(`Comments: ${articleComments.length} | Views: ${usersRelatedToArticle.length}
    | Likes: ${article.articleLikeDtos.length} | Dislikes: ${article.articleDislikeDtos.length}`);

    const buttonElementCardFooter = $("<button>", {
        type: "button",
        'data-formId': "card-form-" + article.articleId,
        class: "btn button-color font-monospace",
        onclick: "submitCardForm()"
    });

    buttonElementCardFooter.text("Read");

    const formElement = $("<form>", {
        id: "card-form-" + article.articleId,
        action: "/Article/ViewArticle",
        class: "shadow rounded",
        method: "post"
    });

    const inputElementArticleId = $("<input>", {
        type: "hidden",
        name: "ArticleId",
        value: article.articleId
    });

    const inputElementArticlePrevTitle = $("<input>", {
        type: "hidden",
        name: "PrevTitle",
        value: article.prevTitle
    });

    const inputElementArticleTitle = $("<input>", {
        type: "hidden",
        name: "Title",
        value: article.title
    });

    const inputElementArticleData = $("<input>", {
        type: "hidden",
        name: "Content",
        value: article.content
    });

    const inputElementArticlePublishedDate = $("<input>", {
        type: "hidden",
        name: "PublishingDate",
        value: article.publishingDate
    });

    const inputElementArticleAuthor = $("<input>", {
        type: "hidden",
        name: "Author",
        value: article.author
    });

    const inputElementArticleLink = $("<input>", {
        type: "hidden",
        name: "Link",
        value: article.link
    });

    const inputElementTopicId = $("<input>", {
        type: "hidden",
        name: "TopicId",
        value: article.topicId
    });

    const inputElementArticleLikes = $("<input>", {
        type: "hidden",
        name: "articleLikeDtos",
        value: JSON.stringify(article.articleLikeDtos)
    });

    const inputElementArticleDislikes = $("<input>", {
        type: "hidden",
        name: "articleDislikeDtos",
        value: JSON.stringify(article.articleDislikeDtos)
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
            return `<span style="background-color: #E5C7EA">${match}</span>`;
        });
    }

    return highlightedText;
}

function highlightMatchingKeywords(textOne, textTwo) {

    //Escape special characters in textTwo for safe use in regular expression.
    const escapedTextTwo = textTwo.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');

    const regex = new RegExp(escapedTextTwo, 'gi');

    //Replace the matches in textOne with <span>...</span> wrapped matches.
    let highlightedText = textOne.replace(regex, (match) => {
        return `<span style="background-color: yellow">${match}</span>`;
    });

    return highlightedText;
}


function ExtractAndHighlightText(textOne, textTwo, textTree = null) {

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

    // Find all matches in textOne
    let matches = textOne.match(regex) || [];

    if (textTree !== null) {

        const escapedTextTree = textTree.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');

        const treeRegex = new RegExp(escapedTextTree, 'gi');

        matches = matches.map((match) => {
            return match.replace(treeRegex, (subMatch) => {
                return `<span style="background-color: #E5C7EA">${subMatch}</span>`;
            });
        });
    }

    return matches;
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
        style: "text-align: center;"
    });

    let tableRows = 0;

    for (const key in tableData) {
        if (tableData.hasOwnProperty(key)) {
           
            const thElement = $("<th>", {
                 scope: "col",
                 style: "text-align: center;",
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
                    style: "text-align: center;" 
                });

                if (tableData[key][i] instanceof jQuery) {
                    // Append the jQuery element (button) to the td.
                    tdElement.append(tableData[key][i]);
                } else {
                    // Set the text if it's not a jQuery element.
                    tdElement.html(tableData[key][i]);
                }

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


function formatDateTime(dateTime) {

    const date = new Date(dateTime);

    const options = {
        weekday: 'long', year: 'numeric', month: 'long', day: 'numeric',
        hour: '2-digit', minute: '2-digit', second: '2-digit', hour12: false,
    };

    return date.toLocaleString('en-US', options);
}