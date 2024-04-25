﻿
//This function creates bootstrap 5 card element for document object, returned from SearchService.
//The card element includes form element with hidden input elements that represent data from the document object.
//Arguments:
// - divElementArticleList: html div element to which the card element will be added.
// - element: document object returned from SearchService.
// - cardTitle: name of the card element.
//Returns: void.
function CreateDocumentCard(divElementArticleList, document_topic_id, element, articleInfo, cardTitle) {

    const divElementCardContainer = $("<div>", {
        class: "col mt-4"
    });

    const divElementCard = $("<div>", {
        class: "card h-100"
    });

    const divElementCardBody = $("<div>", {
        class: "card-body"
    });

    const hElementCardTitle = $("<h5>", {
        class: "card-title"
    });

    hElementCardTitle.text(cardTitle);

    const pElementCardText = $("<p>", {
        class: "card-text"
    });

    pElementCardText.text(element.document.article_prev_title);

    const divElementCardFooter = $("<div>", {
        class: "card-footer d-flex justify-content-between align-items-center"
    });

    const smallElementCardFooterInfo = $("<small>", {
        class: "text-muted"
    });

    smallElementCardFooterInfo.text(`Comments: ${articleInfo.comments.length} | Views: ${articleInfo.views.length} 
    | Likes: ${articleInfo.likes.length} | Dislikes: ${articleInfo.disLikes.length}`);

    const buttonElementCardFooter = $("<button>", {
        type: "button",
        'data-formId': "card-form-" + element.document_id,
        class: "btn btn-primary",
        onclick: "submitCardForm()"
    });

    buttonElementCardFooter.text("Read");

    const formElement = $("<form>", {
        id: "card-form-" + element.document_id,
        action: "/Article/ViewArticle",
        class: "shadow rounded",
        method: "post"
    });

    const inputElementArticleId = $("<input>", {
        type: "hidden",
        name: "ArticleId",
        value: element.document_id
    });

    const inputElementArticlePrevTitle = $("<input>", {
        type: "hidden",
        name: "PrevTitle",
        value: element.document.article_prev_title
    });

    const inputElementArticleTitle = $("<input>", {
        type: "hidden",
        name: "Title",
        value: element.document.article_title
    });

    const inputElementArticleData = $("<input>", {
        type: "hidden",
        name: "Content",
        value: element.document.article_data
    });

    const inputElementArticlePublishedDate = $("<input>", {
        type: "hidden",
        name: "PublishingDate",
        value: element.document.article_published_date
    });

    const inputElementArticleAuthor = $("<input>", {
        type: "hidden",
        name: "Author",
        value: element.document.article_author
    });

    const inputElementArticleLink = $("<input>", {
        type: "hidden",
        name: "Link",
        value: element.document.article_link
    });

    const inputElementTopicId = $("<input>", {
        type: "hidden",
        name: "TopicId",
        value: document_topic_id
    });

    const inputElementArticleComments = $("<input>", {
        type: "hidden",
        name: "articleComments",
        value: JSON.stringify(articleInfo.comments)
    });

    const inputElementUserArticles = $("<input>", {
        type: "hidden",
        name: "userArticleList",
        value: JSON.stringify(articleInfo.views)
    });

    const inputElementArticleLikes = $("<input>", {
        type: "hidden",
        name: "ArticleLikeDtos",
        value: articleInfo.likes
    });

    const inputElementArticleDislikes = $("<input>", {
        type: "hidden",
        name: "ArticleDislikeDtos",
        value: articleInfo.disLikes
    });

    formElement.append(inputElementArticleId);
    formElement.append(inputElementArticlePrevTitle);
    formElement.append(inputElementArticleTitle);
    formElement.append(inputElementArticleData);
    formElement.append(inputElementArticlePublishedDate);
    formElement.append(inputElementArticleAuthor);
    formElement.append(inputElementArticleLink);
    formElement.append(inputElementTopicId);
    formElement.append(inputElementArticleComments);
    formElement.append(inputElementUserArticles);
    formElement.append(inputElementArticleLikes);
    formElement.append(inputElementArticleDislikes);

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
    /// The gi flags make the regular expression global (find all matches) and case-insensitive.
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