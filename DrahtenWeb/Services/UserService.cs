using DrahtenWeb.Dtos.UserService;
using DrahtenWeb.Models;
using DrahtenWeb.Services.IServices;

namespace DrahtenWeb.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IHttpClientFactory httpClientFactory) : base(httpClientFactory) 
        {
        }

        public async Task<TEntity> GetUserByIdAsync<TEntity>(Guid userId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/user-service/users/{userId}",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> GetIssuedBansByUserAsync<TEntity>(Guid userId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/user-service/users/{userId}/issued-bans-by-user/",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> GetReceivedBansByUserAsync<TEntity>(Guid userId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/user-service/users/{userId}/received-bans-by-user/",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> GetIssuedContactRequestsByUserAsync<TEntity>(Guid userId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/user-service/users/{userId}/issued-contact-requests-by-user/",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> GetReceivedContactRequestsByUserAsync<TEntity>(Guid userId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/user-service/users/{userId}/received-contact-requests-by-user/",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> RegisterUserAsync<TEntity>(UserDto userDto, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Url = $"https://localhost:7076/user-service/users/",
                Data = userDto,
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> RegisterBannedUserAsync<TEntity>(BanUserDto banUserDto, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Url = $"https://localhost:7076/user-service/users/{banUserDto.IssuerUserId}/banned-users/{banUserDto.ReceiverUserId}",
                Data = banUserDto,
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> RegisterContactRequestAsync<TEntity>(ContactRequestDto contactRequestDto, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Url = $"https://localhost:7076/user-service/users/{contactRequestDto.IssuerUserId}/contact-requests/{contactRequestDto.ReceiverUserId}",
                Data = contactRequestDto,
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> RegisterUserActivityAsync<TEntity>(UserActivityDto userActivityDto, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Url = $"https://localhost:7076/user-service/users/{userActivityDto.UserId}/user-tracking/",
                Data = userActivityDto,
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> UpdateContactRequestMessageAsync<TEntity>(ContactRequestDto contactRequestDto, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.PUT,
                Url = $"https://localhost:7076/user-service/users/{contactRequestDto.IssuerUserId}/update-contact-request-message/{contactRequestDto.ReceiverUserId}",
                Data = contactRequestDto,
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> RemoveIssuedContactRequestAsync<TEntity>(Guid issuerUserId, Guid receiverUserId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.DELETE,
                Url = $"https://localhost:7076/user-service/users/{issuerUserId}/issued-contact-requests/{receiverUserId}",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> RemoveReceivedContactRequestAsync<TEntity>(Guid receiverUserId, Guid issuerUserId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.DELETE,
                Url = $"https://localhost:7076/user-service/users/{receiverUserId}/received-contact-requests/{issuerUserId}",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> RemoveBannedUserAsync<TEntity>(Guid issuerUserId, Guid receiverUserId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.DELETE,
                Url = $"https://localhost:7076/user-service/users/{issuerUserId}/banned-users/{receiverUserId}",
                AccessToken = accessToken
            });

            return response;
        }
    }
}



//public async Task<TEntity> GetUserById<TEntity>(string userId, string accessToken)
//{
//    var response = await SendAsync<TEntity>(new ApiRequest
//    {

//        ApiType = ApiType.GET,
//        Url = $"https://localhost:7076/user-service/users/{userId}",
//        AccessToken = accessToken
//    });

//    return response;
//}

//public async Task<TEntity> RegisterUser<TEntity>(WriteUserDto user, string accessToken)
//{
//    var response = await SendAsync<TEntity>(new ApiRequest
//    {

//        ApiType = ApiType.POST,
//        Url = "https://localhost:7076/user-service/users/",
//        Data = user,
//        AccessToken = accessToken
//    });

//    return response;
//}

////Change the dto type.
//public async Task<TEntity> RegisterUserTopic<TEntity>(WriteUserTopicDto user, string accessToken)
//{
//    var response = await SendAsync<TEntity>(new ApiRequest
//    {
//        ApiType = ApiType.POST,
//        Url = $"https://localhost:7076/topic-article-service/users/{user.UserId}/topics/",
//        //Url = "https://localhost:7076/user-service/topics/",
//        Data = user,
//        AccessToken = accessToken
//    });

//    return response;
//}

//public async Task<TEntity> GetTopics<TEntity>(string accessToken)
//{
//    var response = await SendAsync<TEntity>(new ApiRequest
//    {

//        ApiType = ApiType.GET,
//        Url = $"https://localhost:7076/topic-article-service/topics/",
//        //Url = "https://localhost:7076/user-service/topics/",
//        AccessToken = accessToken
//    });

//    return response;
//}

//public async Task<TEntity> GetRootTopicWithChildren<TEntity>(Guid topicId, string accessToken)
//{
//    var response = await SendAsync<TEntity>(new ApiRequest
//    {
//        ApiType = ApiType.GET,
//        Url = $"https://localhost:7076/topic-article-service/topics/{topicId}/parent-topic/",
//        //Url = $"https://localhost:7076/user-service/topics/root-topic/{topicId}",
//        AccessToken = accessToken
//    });

//    return response;
//}

//public async Task<TEntity> GetUserTopics<TEntity>(string userId, string accessToken)
//{
//    var response = await SendAsync<TEntity>(new ApiRequest
//    {
//        ApiType = ApiType.GET,
//        Url = $"https://localhost:7076/topic-article-service/topics/{userId}/user-topics/",
//        //Url = $"https://localhost:7076/user-service/topics/user-topics/{userId}",
//        AccessToken = accessToken
//    });

//    return response;
//}

//public async Task<TEntity> RegisterArticle<TEntity>(WriteArticleDto article, string accessToken)
//{
//    var response = await SendAsync<TEntity>(new ApiRequest
//    {
//        ApiType = ApiType.POST,
//        Url = "https://localhost:7076/user-service/articles/article/",
//        Data = article,
//        AccessToken = accessToken
//    });

//    return response;
//}
//public async Task<TEntity> RegisterUserArticle<TEntity>(WriteUserArticleDto userArticle, string accessToken)
//{
//    var response = await SendAsync<TEntity>(new ApiRequest
//    {
//        ApiType = ApiType.POST,
//        Url = "https://localhost:7076/user-service/articles/user-article/",
//        Data = userArticle,
//        AccessToken = accessToken
//    });

//    return response;
//}
//public async Task<TEntity> GetArticle<TEntity>(string articleId, string accessToken)
//{
//    var response = await SendAsync<TEntity>(new ApiRequest
//    {
//        ApiType = ApiType.GET,
//        Url = $"https://localhost:7076/user-service/articles/{articleId}",
//        AccessToken = accessToken
//    });

//    return response;
//}
//public async Task<TEntity> GetArticleLikes<TEntity>(string articleId, string accessToken)
//{
//    var response = await SendAsync<TEntity>(new ApiRequest
//    {
//        ApiType = ApiType.GET,
//        //Url = $"https://localhost:7076/user-service/articles/{articleId}/likes/",
//        AccessToken = accessToken
//    });

//    return response;
//}
//public async Task<TEntity> RegisterArticleLike<TEntity>(WriteArticleLikeDto articleLike, string accessToken)
//{
//    var response = await SendAsync<TEntity>(new ApiRequest
//    {
//        ApiType = ApiType.POST,
//        Url = $"https://localhost:7076/user-service/articles/{articleLike.ArticleId}/likes/{articleLike.UserId}",
//        AccessToken = accessToken
//    });

//    return response;
//}
//public async Task<TEntity> GetArticleComments<TEntity>(string articleId, string accessToken)
//{
//    var response = await SendAsync<TEntity>(new ApiRequest
//    {
//        ApiType = ApiType.GET,
//        Url = $"https://localhost:7076/topic-article-service/articles/{articleId}/comments/",
//        //Url = $"https://localhost:7076/user-service/articles/{articleId}/comments/",
//        AccessToken = accessToken
//    });

//    return response;
//}
//public async Task<TEntity> RegisterArticleComment<TEntity>(WriteArticleCommentDto articleComment, string accessToken)
//{
//    var response = await SendAsync<TEntity>(new ApiRequest
//    {
//        ApiType = ApiType.POST,
//        Url = $"https://localhost:7076/user-service/articles/{articleComment.ArticleId}/comments/",
//        Data = articleComment,
//        AccessToken = accessToken
//    });

//    return response;
//}
//public async Task<TEntity> RegisterArticleCommentThumbsUp<TEntity>(WriteArticleCommentThumbsUpDto articleCommentThumbsUp, string accessToken)
//{
//    var response = await SendAsync<TEntity>(new ApiRequest
//    {
//        ApiType = ApiType.POST,
//        Url = $"https://localhost:7076/user-service/articles/{articleCommentThumbsUp.ArticleId}/comments/{articleCommentThumbsUp.ArticleCommentId}/thumbs-up",
//        Data = articleCommentThumbsUp,
//        AccessToken = accessToken
//    });

//    return response;
//}
//public async Task<TEntity> RegisterArticleCommentThumbsDown<TEntity>(WriteArticleCommentThumbsDownDto articleCommentThumbsDown, string accessToken)
//{
//    var response = await SendAsync<TEntity>(new ApiRequest
//    {
//        ApiType = ApiType.POST,
//        Url = $"https://localhost:7076/user-service/articles/{articleCommentThumbsDown.ArticleId}/comments/{articleCommentThumbsDown.ArticleCommentId}/thumbs-down",
//        Data = articleCommentThumbsDown,
//        AccessToken = accessToken
//    });

//    return response;
//}
//public async Task<TEntity> GetArticleCommentThumbsUp<TEntity>(string articleId, int commentId, string accessToken)
//{
//    var response = await SendAsync<TEntity>(new ApiRequest
//    {
//        ApiType = ApiType.GET,
//        Url = $"https://localhost:7076/user-service/articles/{articleId}/comments/{commentId}/thumbs-up",
//        AccessToken = accessToken
//    });

//    return response;
//}
//public async Task<TEntity> GetArticleCommentThumbsDown<TEntity>(string articleId, int commentId, string accessToken)
//{
//    var response = await SendAsync<TEntity>(new ApiRequest
//    {
//        ApiType = ApiType.GET,
//        Url = $"https://localhost:7076/user-service/articles/{articleId}/comments/{commentId}/thumbs-down",
//        AccessToken = accessToken
//    });

//    return response;
//}
//public async Task<TEntity> DeleteArticleCommentThumbsUp<TEntity>(string articleId, int articleCommentId, string userId, string accessToken)
//{
//    var response = await SendAsync<TEntity>(new ApiRequest
//    {
//        ApiType = ApiType.DELETE,
//        Url = $"https://localhost:7076/user-service/articles/{articleId}/comments/{articleCommentId}/thumbs-up/{userId}",
//        AccessToken = accessToken
//    });

//    return response;
//}
//public async Task<TEntity> DeleteArticleCommentThumbsDown<TEntity>(string articleId, int articleCommentId, string userId, string accessToken)
//{
//    var response = await SendAsync<TEntity>(new ApiRequest
//    {
//        ApiType = ApiType.DELETE,
//        Url = $"https://localhost:7076/user-service/articles/{articleId}/comments/{articleCommentId}/thumbs-down/{userId}",
//        AccessToken = accessToken
//    });

//    return response;
//}
//public async Task<TEntity> GetUsersRelatedToArticle<TEntity>(string articleId, string accessToken)
//{
//    var response = await SendAsync<TEntity>(new ApiRequest
//    {
//        ApiType = ApiType.GET,
//        Url = $"https://localhost:7076/topic-article-service/users/articles/{articleId}",
//        //Url = $"https://localhost:7076/user-service/users/articles/{articleId}",
//        AccessToken = accessToken
//    });

//    return response;
//}
//public async Task<TEntity> GetUserPrivateHistory<TEntity>(string userId, string accessToken)
//{
//    var response = await SendAsync<TEntity>(new ApiRequest
//    {
//        ApiType = ApiType.GET,
//        Url = $"https://localhost:7076/user-service/private-history/{userId}",
//        AccessToken = accessToken
//    });

//    return response;
//}
//public async Task<TEntity> CreateUserPrivateHistory<TEntity>(WritePrivateHistoryDto writePrivateHistoryDto, string accessToken)
//{
//    var response = await SendAsync<TEntity>(new ApiRequest
//    {
//        ApiType = ApiType.POST,
//        Url = $"https://localhost:7076/user-service/private-history/",
//        Data = writePrivateHistoryDto,
//        AccessToken = accessToken
//    });

//    return response;
//}
//public async Task<TEntity> GetUserPublicHistory<TEntity>(string userId, string accessToken)
//{
//    var response = await SendAsync<TEntity>(new ApiRequest
//    {
//        ApiType = ApiType.GET,
//        Url = $"https://localhost:7076/user-service/public-history/{userId}",
//        AccessToken = accessToken
//    });

//    return response;
//}
//public async Task<TEntity> CreateUserPublicHistory<TEntity>(WritePublicHistoryDto writePublicHistoryDto, string accessToken)
//{
//    var response = await SendAsync<TEntity>(new ApiRequest
//    {
//        ApiType = ApiType.POST,
//        Url = $"https://localhost:7076/user-service/public-history/",
//        Data = writePublicHistoryDto,
//        AccessToken = accessToken
//    });

//    return response;
//}
//public async Task<TEntity> GetViewedArticlesPrivateHistory<TEntity>(string userId, string accessToken)
//{
//    var response = await SendAsync<TEntity>(new ApiRequest
//    {
//        ApiType = ApiType.GET,
//        Url = $"https://localhost:7076/user-service/private-history/{userId}/viewed_articles/",
//        AccessToken = accessToken
//    });

//    return response;
//}
//public async Task<TEntity> RegisterViewedArticlePrivateHistory<TEntity>(WriteViewedArticleHistoryDto writeViewedArticleHistoryDto,
//    string accessToken)
//{
//    var response = await SendAsync<TEntity>(new ApiRequest
//    {
//        ApiType = ApiType.POST,
//        Url = $"https://localhost:7076/user-service/private-history/{writeViewedArticleHistoryDto.HistoryId}/viewed_articles/",
//        Data = writeViewedArticleHistoryDto,
//        AccessToken = accessToken
//    });

//    return response;
//}
//public async Task<TEntity> GetSearchedTopicsDataPrivateHistory<TEntity>(string userId, string accessToken)
//{
//    var response = await SendAsync<TEntity>(new ApiRequest
//    {
//        ApiType = ApiType.GET,
//        Url = $"https://localhost:7076/user-service/private-history/{userId}/searched-topics-data/",
//        AccessToken = accessToken
//    });

//    return response;
//}
//public async Task<TEntity> RegisterSearchedTopicsDataPrivateHistory<TEntity>(WriteSearchedTopicDataHistoryDto writeSearchedTopicDataHistoryDto,
//    string accessToken)
//{
//    var response = await SendAsync<TEntity>(new ApiRequest
//    {
//        ApiType = ApiType.POST,
//        Url = $"https://localhost:7076/user-service/private-history/{writeSearchedTopicDataHistoryDto.HistoryId}/searched-topics-data/",
//        Data = writeSearchedTopicDataHistoryDto,
//        AccessToken = accessToken
//    });

//    return response;
//}
