function displayEndDateTime(retentionUntil) {

    const endDateTime = formatDateTime(retentionUntil);

    $('#retentionEndDateTime').text(endDateTime);

    const endDate = new Date(retentionUntil);
    const now = new Date();
    // Calculate the number of days remaining
    const timeDiff = endDate - now;

    const daysRemaining = Math.ceil(timeDiff / (1000 * 60 * 60 * 24));

    $('#daysRemaining').text(`${daysRemaining} days.`);
}

function setHistoryRetention() {

    const retentionPeriod = document.getElementById('historyRetention').value;

    //Send ajax HTTP POST request to /PrivateHistory/SetRetentionPeriod.
    $.ajax({
        type: 'POST',
        url: '/PrivateHistory/SetRetentionPeriod',
        data: {
            'retentionDays': retentionPeriod
        },
        success: (response) => {
            console.log("success", response);
        },
        failure: (reponse) => {
            console.log("failure", response);
        },
        error: (response) => {
            console.log("error", response);
        }
    });
}

function DisplayViewedArticlesPublicHistory(pageNumber = 1) {

    //Send ajax HTTP GET request to /PublicHistory/ViewedArticles.
    $.ajax({
        type: 'GET',
        url: '/PublicHistory/ViewedArticles',
        data: {
            'pageNumber': pageNumber
        },
        success: (response) => {

            if (response != null) {

                $("#historyTableContainer").empty();

                const divElementViewedArticlesTableContainer = $("<div>", {
                    id: "viewedArticlesTableContainer",
                    class: "container mt-1"
                });

                $("#historyTableContainer").append(divElementViewedArticlesTableContainer);

                const tableData = {
                    'Article': [],
                    'Watched': [],
                    'View': [],
                    'Remove': []
                };

                response.articles.forEach((element) => {

                    const buttonElementViewArticle = $("<button>", {
                        type: "button",
                        class: "btn btn-outline-primary btn-sm",
                        html: '<i class="fas fa-eye"></i>'
                    });

                    buttonElementViewArticle.on("click", () => {


                    });


                    const buttonElementRemoveArticle = $("<button>", {
                        type: "button",
                        class: "btn btn-outline-danger btn-sm",
                        html: '<i class="fas fa-trash"></i>'
                    });

                    buttonElementRemoveArticle.on("click", (event) => {


                    });

                    tableData['Article'].push(element.article.title);
                    tableData['Watched'].push(formatDateTime(element.dateTime));
                    tableData['View'].push(buttonElementViewArticle);
                    tableData['Remove'].push(buttonElementRemoveArticle);
                });

                const tableElement = CreateHistoryTable(tableData);

                $("#viewedArticlesTableContainer").append(tableElement);

                if (response.pagination.totalPages > 0) {

                    const divElementPaginationContainer = CreateHistoryTablePagination(response.pagination, "viewedArticlesTablePage");

                    $("#viewedArticlesTableContainer").append(divElementPaginationContainer);
                }
            }
        },
        failure: (reponse) => {
            console.log("failure", response);
        },
        error: (response) => {
            console.log("error", response);
        }
    });
}

function DisplaySearchedViewedArticles(searchedViewedArticles, pageNumber = 1, query = '') {

    //Send ajax HTTP POST request to /PrivateHistory/SearchedViewedArticles.
    $.ajax({
        type: 'POST',
        url: '/PrivateHistory/SearchedViewedArticles',
        data: {
            'searchedViewedArticles': searchedViewedArticles,
            'pageNumber': pageNumber
        },
        success: (response) => {

            if (response != null) {

                $("#historyTableContainer").empty();

                const divElementViewedArticlesTableContainer = $("<div>", {
                    id: "searchedViewedArticlesTableContainer",
                    class: "container mt-1"
                });

                const inputElementSearchedViewedArticles = $("<input>", {
                    id: "searchedViewedArticles",
                    type: "hidden",
                    value: searchedViewedArticles
                });

                $("#historyTableContainer").append(divElementViewedArticlesTableContainer);

                $("#historyTableContainer").append(inputElementSearchedViewedArticles);

                const tableData = {
                    'Article': [],
                    'Watched': [],
                    'View': [],
                    'Remove': [],
                    'Move': []
                };

                response.articles.forEach((element) => {

                    const buttonElementViewArticle = $("<button>", {
                        type: "button",
                        class: "btn btn-outline-primary btn-sm",
                        html: '<i class="fas fa-eye"></i>'
                    });

                    buttonElementViewArticle.on("click", () => {

                        // Serialize the article object to JSON
                        const articleJson = JSON.stringify(element.article);

                        const form = $('<form>', {
                            method: 'POST',
                            action: '/PrivateHistoryArticle/ViewArticle'
                        });

                        const input = $('<input>', {
                            type: 'hidden',
                            name: 'articleJson',
                            value: articleJson
                        });

                        form.append(input);

                        $('body').append(form);

                        // Submit the form
                        form.submit();
                    });


                    const buttonElementRemoveArticle = $("<button>", {
                        type: "button",
                        class: "btn btn-outline-danger btn-sm",
                        html: '<i class="fas fa-trash"></i>'
                    });

                    buttonElementRemoveArticle.on("click", (event) => {

                        // The table row containing the clicked button
                        const row = $(event.currentTarget).closest("tr");

                        // Visual indicator that the row is deleting.
                        row.addClass("fade-out");

                        //Send ajax HTTP DELETE request to /PrivateHistory/RemoveViewedArticle.
                        $.ajax({
                            type: 'DELETE',
                            url: '/PrivateHistory/RemoveViewedArticle',
                            data: {
                                'viewedArticleId': element.viewedArticleId
                            },
                            success: (response) => {

                                // Remove the table row containing the clicked button
                                row.remove();
                            },
                            failure: (reponse) => {
                                console.log("failure", response);
                                row.removeClass("fade-out");
                            },
                            error: (response) => {
                                console.log("error", response);
                                row.removeClass("fade-out");
                            }
                        });
                    });

                    const buttonElementMove = $("<button>", {
                        type: "button",
                        class: "btn btn-outline-success btn-sm",
                        html: '<i class="fas fa-share"></i>'
                    });

                    buttonElementMove.on("click", () => {
                        console.log("Move to public");
                    });

                    const highlightedTitle = highlightMatchingKeywords(element.article.title, query);

                    tableData['Article'].push(highlightedTitle);
                    tableData['Watched'].push(formatDateTime(element.dateTime));
                    tableData['View'].push(buttonElementViewArticle);
                    tableData['Remove'].push(buttonElementRemoveArticle);
                    tableData['Move'].push(buttonElementMove);

                    displayEndDateTime(element.retentionUntil);
                });

                const tableElement = CreateHistoryTable(tableData);

                $("#searchedViewedArticlesTableContainer").append(tableElement);

                if (response.pagination.totalPages > 0) {

                    const divElementPaginationContainer = CreateHistoryTablePagination(response.pagination, "searchedViewedArticlesTablePage");

                    $("#searchedViewedArticlesTableContainer").append(divElementPaginationContainer);
                }
            }
        },
        failure: (reponse) => {
            console.log("failure", response);
        },
        error: (response) => {
            console.log("error", response);
        }
    });
}

function DisplayViewedArticles(pageNumber = 1) {

    //Send ajax HTTP GET request to /PrivateHistory/ViewedArticles.
    $.ajax({
        type: 'GET',
        url: '/PrivateHistory/ViewedArticles',
        data: {
            'pageNumber': pageNumber
        },
        success: (response) => {

            if (response != null) {

                $("#historyTableContainer").empty();

                const divElementViewedArticlesTableContainer = $("<div>", {
                    id: "viewedArticlesTableContainer",
                    class: "container mt-1"
                });

                $("#historyTableContainer").append(divElementViewedArticlesTableContainer);

                const tableData = {
                    'Article': [],
                    'Watched': [],
                    'View': [],
                    'Remove': [],
                    'Move': []
                };

                response.articles.forEach((element) => {

                    const buttonElementViewArticle = $("<button>", {
                        type: "button",
                        class: "btn btn-outline-primary btn-sm",
                        html: '<i class="fas fa-eye"></i>'
                    });

                    buttonElementViewArticle.on("click", () => {

                        // Serialize the article object to JSON
                        const articleJson = JSON.stringify(element.article);

                        const form = $('<form>', {
                            method: 'POST',
                            action: '/PrivateHistoryArticle/ViewArticle'
                        });

                        const input = $('<input>', {
                            type: 'hidden',
                            name: 'articleJson',
                            value: articleJson
                        });

                        form.append(input);

                        $('body').append(form);

                        // Submit the form
                        form.submit();
                    });


                    const buttonElementRemoveArticle = $("<button>", {
                        type: "button",
                        class: "btn btn-outline-danger btn-sm",
                        html: '<i class="fas fa-trash"></i>'
                    });

                    buttonElementRemoveArticle.on("click", (event) => {

                        // The table row containing the clicked button
                        const row = $(event.currentTarget).closest("tr");

                        // Visual indicator that the row is deleting.
                        row.addClass("fade-out");

                        //Send ajax HTTP DELETE request to /PrivateHistory/RemoveViewedArticle.
                        $.ajax({
                            type: 'DELETE',
                            url: '/PrivateHistory/RemoveViewedArticle',
                            data: {
                                'viewedArticleId': element.viewedArticleId
                            },
                            success: (response) => {

                                // Remove the table row containing the clicked button
                                row.remove();
                            },
                            failure: (reponse) => {
                                console.log("failure", response);
                                row.removeClass("fade-out");
                            },
                            error: (response) => {
                                console.log("error", response);
                                row.removeClass("fade-out");
                            }
                        });
                    });

                    const buttonElementMove = $("<button>", {
                        type: "button",
                        class: "btn btn-outline-success btn-sm",
                        html: '<i class="fas fa-share"></i>'
                    });

                    buttonElementMove.on("click", () => {

                        //Send ajax HTTP POST request to /PublicHistory/ViewedArticle.
                        $.ajax({
                            type: 'POST',
                            url: '/PublicHistory/ViewedArticle',
                            data: {
                                'articleId': element.article.articleId,
                                'userId': element.userId,
                                'dateTime': element.dateTime
                            },
                            success: (response) => {

                                console.log("Ok");

                                //TODO: Color the added row
                            },
                            failure: (reponse) => {
                                console.log("failure", response);
                                //row.removeClass("fade-out");
                            },
                            error: (response) => {
                                console.log("error", response);
                                //row.removeClass("fade-out");
                            }
                        });
                    });

                    tableData['Article'].push(element.article.title);
                    tableData['Watched'].push(formatDateTime(element.dateTime));
                    tableData['View'].push(buttonElementViewArticle);
                    tableData['Remove'].push(buttonElementRemoveArticle);
                    tableData['Move'].push(buttonElementMove);

                    displayEndDateTime(element.retentionUntil);
                });

                const tableElement = CreateHistoryTable(tableData);

                $("#viewedArticlesTableContainer").append(tableElement);

                if (response.pagination.totalPages > 0) {

                    const divElementPaginationContainer = CreateHistoryTablePagination(response.pagination, "viewedArticlesTablePage");

                    $("#viewedArticlesTableContainer").append(divElementPaginationContainer);
                }
            }
        },
        failure: (reponse) => {
            console.log("failure", response);
        },
        error: (response) => {
            console.log("error", response);
        }
    });
}


function DisplayViewedUsers(pageNumber = 1) {

    //Send ajax HTTP GET request to /PrivateHistory/ViewedUsers.
    $.ajax({
        type: 'GET',
        url: '/PrivateHistory/ViewedUsers',
        data: {
            'pageNumber': pageNumber
        },
        success: (response) => {

            if (response != null) {

                $("#historyTableContainer").empty();

                const divElementViewedUsersTableContainer = $("<div>", {
                    id: "viewedUsersTableContainer",
                    class: "container mt-1"
                });

                $("#historyTableContainer").append(divElementViewedUsersTableContainer);

                const tableData = {
                    'User': [],
                    'Watched': [],
                    'View': [],
                    'Remove': [],
                    'Move': []
                };

                response.viewedUsers.forEach((element) => {

                    const buttonElementViewUser = $("<button>", {
                        type: "button",
                        class: "btn btn-outline-primary btn-sm",
                        html: '<i class="fas fa-eye"></i>'
                    });

                    buttonElementViewUser.on("click", () => {

                        console.log("View article");
                    });

                    const buttonElementRemoveUser = $("<button>", {
                        type: "button",
                        class: "btn btn-outline-danger btn-sm",
                        html: '<i class="fas fa-trash"></i>'
                    });

                    buttonElementRemoveUser.on("click", () => {
                        console.log("Remove article");
                    });

                    const buttonElementMove = $("<button>", {
                        type: "button",
                        class: "btn btn-outline-success btn-sm",
                        html: '<i class="fas fa-share"></i>'
                    });

                    buttonElementMove.on("click", () => {
                        console.log("Move to public");
                    });


                    tableData['User'].push(element.viewedUser.userNickName);
                    tableData['Watched'].push(formatDateTime(element.dateTime));
                    tableData['View'].push(buttonElementViewUser);
                    tableData['Remove'].push(buttonElementRemoveUser);
                    tableData['Move'].push(buttonElementMove);

                    displayEndDateTime(element.retentionUntil);
                });

                const tableElement = CreateHistoryTable(tableData);

                $("#viewedUsersTableContainer").append(tableElement);

                if (response.pagination.totalPages > 0) {

                    const divElementPaginationContainer = CreateHistoryTablePagination(response.pagination, "viewedUsersTablePage");

                    $("#viewedUsersTableContainer").append(divElementPaginationContainer);
                }
            }
        },
        failure: (reponse) => {
            console.log("failure", response);
        },
        error: (response) => {
            console.log("error", response);
        }
    });
}


function DisplaySearchedArticleData(pageNumber = 1) {

    //Send ajax HTTP GET request to /PrivateHistory/SearchedArticleData.
    $.ajax({
        type: 'GET',
        url: '/PrivateHistory/SearchedArticleData',
        data: {
            'pageNumber': pageNumber
        },
        success: (response) => {

            if (response != null) {

                $("#historyTableContainer").empty();

                const divElementSearchedArticleDataTableContainer = $("<div>", {
                    id: "searchedArticleDataTableContainer",
                    class: "container mt-1"
                });

                $("#historyTableContainer").append(divElementSearchedArticleDataTableContainer);

                const tableData = {
                    'Article': [],
                    'Searched Data': [],
                    'Search Time': [],
                    'View': [],
                    'Remove': [],
                    'Move': []
                };

                response.searchedArticles.forEach((element) => {

                    const buttonElementViewArticle = $("<button>", {
                        type: "button",
                        class: "btn btn-outline-primary btn-sm",
                        html: '<i class="fas fa-eye"></i>'
                    });

                    buttonElementViewArticle.on("click", () => {

                        console.log("View article");
                    });

                    const buttonElementRemoveSearchedArticle = $("<button>", {
                        type: "button",
                        class: "btn btn-outline-danger btn-sm",
                        html: '<i class="fas fa-trash"></i>'
                    });

                    buttonElementRemoveSearchedArticle.on("click", (event) => {

                        // The table row containing the clicked button
                        const row = $(event.currentTarget).closest("tr");

                        // Visual indicator that the row is deleting.
                        row.addClass("fade-out");

                        //Send ajax HTTP DELETE request to /PrivateHistory/RemoveSearchedArticleData.
                        $.ajax({
                            type: 'DELETE',
                            url: '/PrivateHistory/RemoveSearchedArticleData',
                            data: {
                                'searchedArticleDataId': element.searchedArticleDataId
                            },
                            success: (response) => {

                                // Remove the table row containing the clicked button
                                row.remove();
                            },
                            failure: (reponse) => {
                                console.log("failure", response);
                                row.removeClass("fade-out");
                            },
                            error: (response) => {
                                console.log("error", response);
                                row.removeClass("fade-out");
                            }
                        });
                    });

                    const buttonElementMove = $("<button>", {
                        type: "button",
                        class: "btn btn-outline-success btn-sm",
                        html: '<i class="fas fa-share"></i>'
                    });

                    buttonElementMove.on("click", () => {

                        //Send ajax HTTP POST request to /PublicHistory/SearchedArticleData.
                        $.ajax({
                            type: 'POST',
                            url: '/PublicHistory/SearchedArticleData',
                            data: {
                                'articleId': element.article.articleId,
                                'userId': element.userId,
                                'searchedData': element.searchedData,
                                'dateTime': element.dateTime
                            },
                            success: (response) => {

                                console.log("Ok");

                                //TODO: Color the added row
                            },
                            failure: (reponse) => {
                                console.log("failure", response);
                                //row.removeClass("fade-out");
                            },
                            error: (response) => {
                                console.log("error", response);
                                //row.removeClass("fade-out");
                            }
                        });
                    });


                    tableData['Article'].push(element.article.title);
                    tableData['Searched Data'].push(element.searchedData);
                    tableData['Search Time'].push(formatDateTime(element.dateTime));
                    tableData['View'].push(buttonElementViewArticle);
                    tableData['Remove'].push(buttonElementRemoveSearchedArticle);
                    tableData['Move'].push(buttonElementMove);

                    displayEndDateTime(element.retentionUntil);
                });

                const tableElement = CreateHistoryTable(tableData);

                $("#searchedArticleDataTableContainer").append(tableElement);

                if (response.pagination.totalPages > 0) {

                    const divElementPaginationContainer = CreateHistoryTablePagination(response.pagination, "searchedArticleDataTablePage");

                    $("#searchedArticleDataTableContainer").append(divElementPaginationContainer);
                }
            }
        },
        failure: (reponse) => {
            console.log("failure", response);
        },
        error: (response) => {
            console.log("error", response);
        }
    });
}


function DisplayLikedArticles(pageNumber = 1) {

    //Send ajax HTTP GET request to /PrivateHistory/LikedArticles.
    $.ajax({
        type: 'GET',
        url: '/PrivateHistory/LikedArticles',
        data: {
            'pageNumber': pageNumber
        },
        success: (response) => {

            if (response != null) {

                $("#historyTableContainer").empty();

                const divElementLikedArticlesTableContainer = $("<div>", {
                    id: "likedArticlesTableContainer",
                    class: "container mt-1"
                });

                $("#historyTableContainer").append(divElementLikedArticlesTableContainer);

                const tableData = {
                    'Article': [],
                    'Time': [],
                    'Remove': []
                };

                response.likedArticles.forEach((element) => {

                    const buttonElementRemoveLikedArticle = $("<button>", {
                        type: "button",
                        class: "btn btn-outline-danger btn-sm",
                        html: '<i class="fas fa-trash"></i>'
                    });

                    buttonElementRemoveLikedArticle.on("click", () => {
                        console.log("Remove article");
                    });

                    tableData['Article'].push(element.article.title);
                    tableData['Time'].push(formatDateTime(element.dateTime));
                    tableData['Remove'].push(buttonElementRemoveLikedArticle);

                    displayEndDateTime(element.retentionUntil);
                });

                const tableElement = CreateHistoryTable(tableData);

                $("#likedArticlesTableContainer").append(tableElement);

                if (response.pagination.totalPages > 0) {

                    const divElementPaginationContainer = CreateHistoryTablePagination(response.pagination, "likedArticlesTablePage");

                    $("#likedArticlesTableContainer").append(divElementPaginationContainer);
                }
            }
        },
        failure: (reponse) => {
            console.log("failure", response);
        },
        error: (response) => {
            console.log("error", response);
        }
    });
}


function DisplayDislikedArticles(pageNumber = 1) {

    //Send ajax HTTP GET request to /PrivateHistory/DislikedArticles.
    $.ajax({
        type: 'GET',
        url: '/PrivateHistory/DislikedArticles',
        data: {
            'pageNumber': pageNumber
        },
        success: (response) => {

            if (response != null) {

                $("#historyTableContainer").empty();

                const divElementDislikedArticlesTableContainer = $("<div>", {
                    id: "dislikedArticlesTableContainer",
                    class: "container mt-1"
                });

                $("#historyTableContainer").append(divElementDislikedArticlesTableContainer);

                const tableData = {
                    'Article': [],
                    'Time': [],
                    'Remove': []
                };

                response.dislikedArticles.forEach((element) => {

                    const buttonElementRemoveDislikedArticle = $("<button>", {
                        type: "button",
                        class: "btn btn-outline-danger btn-sm",
                        html: '<i class="fas fa-trash"></i>'
                    });

                    buttonElementRemoveDislikedArticle.on("click", () => {
                        console.log("Remove article");
                    });

                    tableData['Article'].push(element.article.title);
                    tableData['Time'].push(formatDateTime(element.dateTime));
                    tableData['Remove'].push(buttonElementRemoveDislikedArticle);

                    displayEndDateTime(element.retentionUntil);
                });

                const tableElement = CreateHistoryTable(tableData);

                $("#dislikedArticlesTableContainer").append(tableElement);

                if (response.pagination.totalPages > 0) {

                    const divElementPaginationContainer = CreateHistoryTablePagination(response.pagination, "dislikedArticlesTablePage");

                    $("#dislikedArticlesTableContainer").append(divElementPaginationContainer);
                }
            }
        },
        failure: (reponse) => {
            console.log("failure", response);
        },
        error: (response) => {
            console.log("error", response);
        }
    });
}

function DisplayCommentedArticles(pageNumber = 1) {

    //Send ajax HTTP GET request to /PrivateHistory/CommentedArticles.
    $.ajax({
        type: 'GET',
        url: '/PrivateHistory/CommentedArticles',
        data: {
            'pageNumber': pageNumber
        },
        success: (response) => {

            if (response != null) {

                $("#historyTableContainer").empty();

                const divElementCommentedArticlesTableContainer = $("<div>", {
                    id: "commentedArticlesTableContainer",
                    class: "container mt-1"
                });

                $("#historyTableContainer").append(divElementCommentedArticlesTableContainer);

                const tableData = {
                    'Article': [],
                    'Comment': [],
                    'Time': [],
                    'Remove': [],
                    'Move': []
                };

                response.commentedArticles.forEach((element) => {

                    const buttonElementRemoveCommentedArticle = $("<button>", {
                        type: "button",
                        class: "btn btn-outline-danger btn-sm",
                        html: '<i class="fas fa-trash"></i>'
                    });

                    buttonElementRemoveCommentedArticle.on("click", (event) => {

                        // The table row containing the clicked button
                        const row = $(event.currentTarget).closest("tr");

                        // Visual indicator that the row is deleting.
                        row.addClass("fade-out");

                        //Send ajax HTTP DELETE request to /PrivateHistory/RemoveCommentedArticle.
                        $.ajax({
                            type: 'DELETE',
                            url: '/PrivateHistory/RemoveCommentedArticle',
                            data: {
                                'commentedArticleId': element.commentedArticleId
                            },
                            success: (response) => {

                                // Remove the table row containing the clicked button
                                row.remove();
                            },
                            failure: (reponse) => {
                                console.log("failure", response);
                                row.removeClass("fade-out");
                            },
                            error: (response) => {
                                console.log("error", response);
                                row.removeClass("fade-out");
                            }
                        });
                    });

                    const buttonElementMove = $("<button>", {
                        type: "button",
                        class: "btn btn-outline-success btn-sm",
                        html: '<i class="fas fa-share"></i>'
                    });

                    buttonElementMove.on("click", () => {

                        //Send ajax HTTP POST request to /PublicHistory/CommentedArticle.
                        $.ajax({
                            type: 'POST',
                            url: '/PublicHistory/CommentedArticle',
                            data: {
                                'articleId': element.article.articleId,
                                'userId': element.userId,
                                'articleComment': element.articleComment,
                                'dateTime': element.dateTime
                            },
                            success: (response) => {

                                //TODO: Color the added row
                            },
                            failure: (reponse) => {
                                console.log("failure", response);
                                //row.removeClass("fade-out");
                            },
                            error: (response) => {
                                console.log("error", response);
                                //row.removeClass("fade-out");
                            }
                        });
                    });

                    tableData['Article'].push(element.article.title);
                    tableData['Comment'].push(element.articleComment);
                    tableData['Time'].push(formatDateTime(element.dateTime));
                    tableData['Remove'].push(buttonElementRemoveCommentedArticle);
                    tableData['Move'].push(buttonElementMove);

                    displayEndDateTime(element.retentionUntil);
                });

                const tableElement = CreateHistoryTable(tableData);

                $("#commentedArticlesTableContainer").append(tableElement);

                if (response.pagination.totalPages > 0) {

                    const divElementPaginationContainer = CreateHistoryTablePagination(response.pagination, "commentedArticlesTablePage");

                    $("#commentedArticlesTableContainer").append(divElementPaginationContainer);
                }
            }
        },
        failure: (reponse) => {
            console.log("failure", response);
        },
        error: (response) => {
            console.log("error", response);
        }
    });
}


function DisplayCommentedArticlesPublicHistory(pageNumber = 1) {

    //Send ajax HTTP GET request to /PublicHistory/CommentedArticles.
    $.ajax({
        type: 'GET',
        url: '/PublicHistory/CommentedArticles',
        data: {
            'pageNumber': pageNumber
        },
        success: (response) => {

            if (response != null) {

                $("#historyTableContainer").empty();

                const divElementCommentedArticlesTableContainer = $("<div>", {
                    id: "commentedArticlesTableContainer",
                    class: "container mt-1"
                });

                $("#historyTableContainer").append(divElementCommentedArticlesTableContainer);

                const tableData = {
                    'Article': [],
                    'Comment': [],
                    'Time': [],
                    'Remove': []
                };

                response.commentedArticles.forEach((element) => {

                    const buttonElementRemoveCommentedArticle = $("<button>", {
                        type: "button",
                        class: "btn btn-outline-danger btn-sm",
                        html: '<i class="fas fa-trash"></i>'
                    });

                    buttonElementRemoveCommentedArticle.on("click", (event) => {


                    });

                    tableData['Article'].push(element.article.title);
                    tableData['Comment'].push(element.articleComment);
                    tableData['Time'].push(formatDateTime(element.dateTime));
                    tableData['Remove'].push(buttonElementRemoveCommentedArticle);
                });

                const tableElement = CreateHistoryTable(tableData);

                $("#commentedArticlesTableContainer").append(tableElement);

                if (response.pagination.totalPages > 0) {

                    const divElementPaginationContainer = CreateHistoryTablePagination(response.pagination, "commentedArticlesTablePage");

                    $("#commentedArticlesTableContainer").append(divElementPaginationContainer);
                }
            }
        },
        failure: (reponse) => {
            console.log("failure", response);
        },
        error: (response) => {
            console.log("error", response);
        }
    });
}


function DisplayLikedArticleComments(pageNumber = 1) {

    //Send ajax HTTP GET request to /PrivateHistory/LikedArticleComments.
    $.ajax({
        type: 'GET',
        url: '/PrivateHistory/LikedArticleComments',
        data: {
            'pageNumber': pageNumber
        },
        success: (response) => {

            if (response != null) {

                $("#historyTableContainer").empty();

                const divElementLikedArticleCommentsTableContainer = $("<div>", {
                    id: "likedArticleCommentsTableContainer",
                    class: "container mt-1"
                });

                $("#historyTableContainer").append(divElementLikedArticleCommentsTableContainer);

                const tableData = {
                    'Article': [],
                    'Article Comment': [],
                    'Time': [],
                    'Remove': []
                };

                response.likedArticleComments.forEach((element) => {

                    const buttonElementRemoveLikedArticleComment = $("<button>", {
                        type: "button",
                        class: "btn btn-outline-danger btn-sm",
                        html: '<i class="fas fa-trash"></i>'
                    });

                    buttonElementRemoveLikedArticleComment.on("click", () => {
                        console.log("Remove article");
                    });

                    tableData['Article'].push(element.article.title);
                    tableData['Article Comment'].push(element.articleCommentId);
                    tableData['Time'].push(formatDateTime(element.dateTime));
                    tableData['Remove'].push(buttonElementRemoveLikedArticleComment);

                    displayEndDateTime(element.retentionUntil);
                });

                const tableElement = CreateHistoryTable(tableData);

                $("#likedArticleCommentsTableContainer").append(tableElement);

                if (response.pagination.totalPages > 0) {

                    const divElementPaginationContainer = CreateHistoryTablePagination(response.pagination, "likedArticleCommentsTablePage");

                    $("#likedArticleCommentsTableContainer").append(divElementPaginationContainer);
                }
            }
        },
        failure: (reponse) => {
            console.log("failure", response);
        },
        error: (response) => {
            console.log("error", response);
        }
    });
}


function DisplayDislikedArticleComments(pageNumber = 1) {

    //Send ajax HTTP GET request to /PrivateHistory/DislikedArticleComments.
    $.ajax({
        type: 'GET',
        url: '/PrivateHistory/DislikedArticleComments',
        data: {
            'pageNumber': pageNumber
        },
        success: (response) => {

            if (response != null) {

                $("#historyTableContainer").empty();

                const divElementDislikedArticleCommentsTableContainer = $("<div>", {
                    id: "dislikedArticleCommentsTableContainer",
                    class: "container mt-1"
                });

                $("#historyTableContainer").append(divElementDislikedArticleCommentsTableContainer);

                const tableData = {
                    'Article': [],
                    'Article Comment': [],
                    'Time': [],
                    'Remove': []
                };

                response.dislikedArticleComments.forEach((element) => {

                    const buttonElementRemoveDislikedArticleComment = $("<button>", {
                        type: "button",
                        class: "btn btn-outline-danger btn-sm",
                        html: '<i class="fas fa-trash"></i>'
                    });

                    buttonElementRemoveDislikedArticleComment.on("click", () => {
                        console.log("Remove article");
                    });

                    tableData['Article'].push(element.article.title);
                    tableData['Article Comment'].push(element.articleCommentId);
                    tableData['Time'].push(formatDateTime(element.dateTime));
                    tableData['Remove'].push(buttonElementRemoveDislikedArticleComment);

                    displayEndDateTime(element.retentionUntil);
                });

                const tableElement = CreateHistoryTable(tableData);

                $("#dislikedArticleCommentsTableContainer").append(tableElement);

                if (response.pagination.totalPages > 0) {

                    const divElementPaginationContainer = CreateHistoryTablePagination(response.pagination, "dislikedArticleCommentsTablePage");

                    $("#dislikedArticleCommentsTableContainer").append(divElementPaginationContainer);
                }
            }
        },
        failure: (reponse) => {
            console.log("failure", response);
        },
        error: (response) => {
            console.log("error", response);
        }
    });
}


function DisplaySearchedTopicData(pageNumber = 1) {

    //Send ajax HTTP GET request to /PrivateHistory/SearchedTopicData.
    $.ajax({
        type: 'GET',
        url: '/PrivateHistory/SearchedTopicData',
        data: {
            'pageNumber': pageNumber
        },
        success: (response) => {

            if (response != null) {

                $("#historyTableContainer").empty();

                const divElementSearchedTopicDataTableContainer = $("<div>", {
                    id: "searchedTopicDataTableContainer",
                    class: "container mt-1"
                });

                $("#historyTableContainer").append(divElementSearchedTopicDataTableContainer);

                const tableData = {
                    'Topic': [],
                    'Searched Data': [],
                    'Search Time': [],
                    'Remove': [],
                    'Move To Public': []
                };

                response.searchedTopics.forEach((element) => {

                    tableData['Topic'].push(element.topicId);
                    tableData['Searched Data'].push(element.searchedData);
                    tableData['Search Time'].push(element.dateTime);
                    tableData['Remove'].push('Remove');
                    tableData['Move To Public'].push('Remove');

                    displayEndDateTime(element.retentionUntil);
                });

                const tableElement = CreateHistoryTable(tableData);

                $("#searchedTopicDataTableContainer").append(tableElement);

                if (response.pagination.totalPages > 0) {

                    const divElementPaginationContainer = CreateHistoryTablePagination(response.pagination, "searchedTopicDataTablePage");

                    $("#searchedTopicDataTableContainer").append(divElementPaginationContainer);
                }
            }
        },
        failure: (reponse) => {
            console.log("failure", response);
        },
        error: (response) => {
            console.log("error", response);
        }
    });
}


function DisplayTopicSubscriptions(pageNumber = 1) {

    //Send ajax HTTP GET request to /PrivateHistory/TopicSubscriptions.
    $.ajax({
        type: 'GET',
        url: '/PrivateHistory/TopicSubscriptions',
        data: {
            'pageNumber': pageNumber
        },
        success: (response) => {

            if (response != null) {

                $("#historyTableContainer").empty();

                const divElementTopicSubscriptionsTableContainer = $("<div>", {
                    id: "topicSubscriptionsTableContainer",
                    class: "container mt-1"
                });

                $("#historyTableContainer").append(divElementTopicSubscriptionsTableContainer);

                const tableData = {
                    'Topic': [],
                    'Time': [],
                    'Remove': []
                };

                response.topicSubscriptions.forEach((element) => {

                    tableData['Topic'].push(element.topicId);
                    tableData['Time'].push(element.dateTime);
                    tableData['Remove'].push('Remove');

                    displayEndDateTime(element.retentionUntil);
                });

                const tableElement = CreateHistoryTable(tableData);

                $("#topicSubscriptionsTableContainer").append(tableElement);

                if (response.pagination.totalPages > 0) {

                    const divElementPaginationContainer = CreateHistoryTablePagination(response.pagination, "topicSubscriptionsTablePage");

                    $("#topicSubscriptionsTableContainer").append(divElementPaginationContainer);
                }
            }
        },
        failure: (reponse) => {
            console.log("failure", response);
        },
        error: (response) => {
            console.log("error", response);
        }
    });
}