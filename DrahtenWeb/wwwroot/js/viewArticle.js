function registerUserComment(articleCommentId) {

    if ($("#user-comment-inputGroup-" + articleCommentId).length == 0) {

        const divElementInputGroup = $("<div>", {
            id: "user-comment-inputGroup-" + articleCommentId,
            class: "input-group mt-2 mb-3"
        });

        const textareaElementComment = $("<textarea>", {
            class: "form-control comment-textarea",
            placeholder: "Write a comment..."
        });

        const spanElementButtonHolder = $("<span>", {
            class: "ms-1"
        });

        const buttonElementComment = $("<button>", {
            type: "button",
            class: "btn btn-outline-dark btn-sm",
            text: "Comment"
        });

        buttonElementComment.on("click", () => {

            const articleId = $("#articleId").val();
            const userComment = textareaElementComment.val();

            //Send ajax HTTP GET request to /Article/ArticleChildComment.
            $.ajax({
                type: 'POST',
                url: '/Article/ArticleChildComment',
                data: {
                    'articleId': articleId,
                    'parentArticleCommentId': articleCommentId, //this is the id of the parent comment.
                    'comment': userComment
                },
                success: (response) => {
                    //TODO: Load the response.
                },
                complete: () => {

                },
                failure: (reponse) => {
                    console.log("failure", response);
                },
                error: (response) => {
                    console.log("error", response);
                }
            });
        });

        const buttonElementCommentCancel = $("<button>", {
            type: "button",
            class: "btn btn-outline-dark btn-sm",
            text: "Cancel"
        });

        buttonElementCommentCancel.on("click", () => {


        });

        spanElementButtonHolder.append(buttonElementComment);
        spanElementButtonHolder.append(buttonElementCommentCancel);
        divElementInputGroup.append(textareaElementComment);
        divElementInputGroup.append(spanElementButtonHolder);

        $("#comment-actions-" + articleCommentId).append(divElementInputGroup);
    }
}

function registerUserCommentLike(articleCommentId) {

    this.event.target.style.color = "green";

    $("#thumbs-down-" + articleCommentId).css("color", "black");

    //Send ajax HTTP POST request to /Article/ArticleCommentLike.
    $.ajax({
        type: 'POST',
        url: '/Article/ArticleCommentLike',
        data: {
            'articleCommentId': articleCommentId
        },
        success: (response) => {
            //TODO: Load the response.
        },
        complete: () => {

        },
        failure: (reponse) => {
            console.log("failure", response);
        },
        error: (response) => {
            console.log("error", response);
        }
    });
}

function registerUserCommentDislike(articleCommentId) {

    this.event.target.style.color = "red";

    $("#thumbs-up-" + articleCommentId).css("color", "black");

    //Send ajax HTTP POST request to /Article/ArticleCommentDislike.
    $.ajax({
        type: 'POST',
        url: '/Article/ArticleCommentDislike',
        data: {
            'articleCommentId': articleCommentId
        },
        success: (response) => {
            //TODO: Load the response.
        },
        complete: () => {

        },
        failure: (reponse) => {
            console.log("failure", response);
        },
        error: (response) => {
            console.log("error", response);
        }
    });
}