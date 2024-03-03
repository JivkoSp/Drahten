using DrahtenWeb.Dtos;
using DrahtenWeb.Models;
using DrahtenWeb.Services.IServices;
using System.ComponentModel.Design;

namespace DrahtenWeb.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IHttpClientFactory httpClientFactory) : base(httpClientFactory) 
        {
           
        }

        public async Task<TEntity> GetUserById<TEntity>(string userId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest { 
            
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/user-service/users/{userId}",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> RegisterUser<TEntity>(WriteUserDto user, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest { 
                
                ApiType = ApiType.POST,
                Url = "https://localhost:7076/user-service/users/",
                Data = user,
                AccessToken = accessToken
            });

            return response;
        }

        //Change the dto type.
        public async Task<TEntity> RegisterUserTopic<TEntity>(WriteUserTopicDto user, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Url = "https://localhost:7076/user-service/topics/",
                Data = user,
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> GetTopics<TEntity>(string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest {
            
                ApiType = ApiType.GET,
                Url = "https://localhost:7076/user-service/topics/",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> GetRootTopicWithChildren<TEntity>(int topicId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/user-service/topics/root-topic/{topicId}",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> GetUserTopics<TEntity>(string userId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/user-service/topics/user-topics/{userId}",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> RegisterArticle<TEntity>(WriteArticleDto article, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Url = "https://localhost:7076/user-service/articles/article/",
                Data = article,
                AccessToken = accessToken
            });

            return response;
        }
        public async Task<TEntity> RegisterUserArticle<TEntity>(WriteUserArticleDto userArticle, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Url = "https://localhost:7076/user-service/articles/user-article/",
                Data = userArticle,
                AccessToken = accessToken
            });

            return response;
        }
        public async Task<TEntity> GetArticle<TEntity>(string articleId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/user-service/articles/{articleId}",
                AccessToken = accessToken
            });

            return response;
        }
        public async Task<TEntity> GetArticleLikes<TEntity>(string articleId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/user-service/articles/{articleId}/likes/",
                AccessToken = accessToken
            });

            return response;
        }
        public async Task<TEntity> RegisterArticleLike<TEntity>(WriteArticleLikeDto articleLike, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Url = $"https://localhost:7076/user-service/articles/{articleLike.ArticleId}/likes/{articleLike.UserId}",
                AccessToken = accessToken
            });

            return response;
        }
        public async Task<TEntity> GetArticleComments<TEntity>(string articleId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/user-service/articles/{articleId}/comments/",
                AccessToken = accessToken
            });

            return response;
        }
        public async Task<TEntity> RegisterArticleComment<TEntity>(WriteArticleCommentDto articleComment, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Url = $"https://localhost:7076/user-service/articles/{articleComment.ArticleId}/comments/",
                Data = articleComment,
                AccessToken = accessToken
            });

            return response;
        }
        public async Task<TEntity> RegisterArticleCommentThumbsUp<TEntity>(WriteArticleCommentThumbsUpDto articleCommentThumbsUp, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Url = $"https://localhost:7076/user-service/articles/{articleCommentThumbsUp.ArticleId}/comments/{articleCommentThumbsUp.ArticleCommentId}/thumbs-up",
                Data = articleCommentThumbsUp,
                AccessToken = accessToken
            });

            return response;
        }
        public async Task<TEntity> RegisterArticleCommentThumbsDown<TEntity>(WriteArticleCommentThumbsDownDto articleCommentThumbsDown, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Url = $"https://localhost:7076/user-service/articles/{articleCommentThumbsDown.ArticleId}/comments/{articleCommentThumbsDown.ArticleCommentId}/thumbs-down",
                Data = articleCommentThumbsDown,
                AccessToken = accessToken
            });

            return response;
        }
        public async Task<TEntity> GetArticleCommentThumbsUp<TEntity>(string articleId,int commentId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/user-service/articles/{articleId}/comments/{commentId}/thumbs-up",
                AccessToken = accessToken
            });

            return response;
        }
        public async Task<TEntity> GetArticleCommentThumbsDown<TEntity>(string articleId,int commentId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/user-service/articles/{articleId}/comments/{commentId}/thumbs-down",
                AccessToken = accessToken
            });

            return response;
        }
        public async Task<TEntity> DeleteArticleCommentThumbsUp<TEntity>(string articleId, int articleCommentId, string userId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.DELETE,
                Url = $"https://localhost:7076/user-service/articles/{articleId}/comments/{articleCommentId}/thumbs-up/{userId}",
                AccessToken = accessToken
            });

            return response;
        }
        public async Task<TEntity> DeleteArticleCommentThumbsDown<TEntity>(string articleId, int articleCommentId, string userId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.DELETE,
                Url = $"https://localhost:7076/user-service/articles/{articleId}/comments/{articleCommentId}/thumbs-down/{userId}",
                AccessToken = accessToken
            });

            return response;
        }
        public async Task<TEntity> GetUsersRelatedToArticle<TEntity>(string articleId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/user-service/users/articles/{articleId}",
                AccessToken = accessToken
            });

            return response;
        }
    }
}
