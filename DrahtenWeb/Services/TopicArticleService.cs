using DrahtenWeb.Dtos.TopicArticleService;
using DrahtenWeb.Models;
using DrahtenWeb.Services.IServices;

namespace DrahtenWeb.Services
{
    public class TopicArticleService : BaseService, ITopicArticleService
    {
        public TopicArticleService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<TEntity> GetTopicsAsync<TEntity>(string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/topic-article-service/topics/",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> GetParentTopicWithChildrenAsync<TEntity>(Guid topicId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/topic-article-service/topics/{topicId}/parent-topic/",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> GetTopicsRelatedToUserAsync<TEntity>(Guid userId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/topic-article-service/topics/{userId}/user-topics/",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> GetArticleByIdAsync<TEntity>(Guid articleId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/topic-article-service/articles/{articleId}",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> GetArticleLikesAsync<TEntity>(Guid articleId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/topic-article-service/articles/{articleId}/likes/",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> GetArticleDislikesAsync<TEntity>(Guid articleId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/topic-article-service/articles/{articleId}/dislikes/",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> GetUsersRelatedToArticleAsync<TEntity>(Guid articleId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/topic-article-service/users/articles/{articleId}",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> GetArticleCommentsAsync<TEntity>(Guid articleId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/topic-article-service/articles/{articleId}/comments/",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> RegisterArticleAsync<TEntity>(WriteArticleDto writeArticleDto, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Url = $"https://localhost:7076/topic-article-service/articles/",
                Data = writeArticleDto,
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> RegisterArticleLikeAsync<TEntity>(ArticleLikeDto articleLikeDto, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Url = $"https://localhost:7076/topic-article-service/articles/{articleLikeDto.ArticleId}/likes/",
                Data = articleLikeDto,
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> RegisterArticleDislikeAsync<TEntity>(ArticleDislikeDto articleDislikeDto, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Url = $"https://localhost:7076/topic-article-service/articles/{articleDislikeDto.ArticleId}/dislikes/",
                Data = articleDislikeDto,
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> RegisterArticleCommentAsync<TEntity>(WriteArticleCommentDto writeArticleCommentDto, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Url = $"https://localhost:7076/topic-article-service/articles/{writeArticleCommentDto.ArticleId}/comments/",
                Data = writeArticleCommentDto,
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> RegisterArticleCommentLikeAsync<TEntity>(ArticleCommentLikeDto articleCommentLikeDto, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Url = $"https://localhost:7076/topic-article-service/articles/comments/{articleCommentLikeDto.ArticleCommentId}/likes/",
                Data = articleCommentLikeDto,
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> RegisterArticleCommentDislikeAsync<TEntity>(ArticleCommentDislikeDto articleCommentDislikeDto, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Url = $"https://localhost:7076/topic-article-service/articles/comments/{articleCommentDislikeDto.ArticleCommentId}/dislikes/",
                Data = articleCommentDislikeDto,
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> RemoveArticleCommentAsync<TEntity>(Guid articleId, Guid articleCommentId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.DELETE,
                Url = $"https://localhost:7076/topic-article-service/articles/{articleId}/comments/{articleCommentId}",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> RegisterUserArticleAsync<TEntity>(WriteUserArticleDto userArticleDto, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Url = $"https://localhost:7076/topic-article-service/users/{userArticleDto.UserId}/articles/",
                Data = userArticleDto,
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> RegisterUserTopicAsync<TEntity>(WriteUserTopicDto writeUserTopicDto, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Url = $"https://localhost:7076/topic-article-service/users/{writeUserTopicDto.UserId}/topics/",
                Data = writeUserTopicDto,
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> RegisterUserAsync<TEntity>(Guid userId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Url = $"https://localhost:7076/topic-article-service/users/",
                Data = userId,
                AccessToken = accessToken
            });

            return response;
        }
    }
}
