using DrahtenWeb.Models;
using DrahtenWeb.Services.IServices;

namespace DrahtenWeb.Services
{
    public class PublicHistoryService : BaseService, IPublicHistoryService
    {
        public PublicHistoryService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<TEntity> GetCommentedArticlesAsync<TEntity>(Guid userId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/publichistory-service/users/{userId}/commented-articles/",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> GetSearchedArticlesAsync<TEntity>(Guid userId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/publichistory-service/users/{userId}/searched-articles/",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> GetSearchedTopicsAsync<TEntity>(Guid userId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/publichistory-service/users/{userId}/searched-topics/",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> GetViewedArticlesAsync<TEntity>(Guid userId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/publichistory-service/users/{userId}/viewed-articles/",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> GetViewedUsersAsync<TEntity>(Guid userId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/publichistory-service/users/{userId}/viewed-users/",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> RemoveCommentedArticleAsync<TEntity>(Guid userId, Guid commentedArticleId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.DELETE,
                Url = $"https://localhost:7076/publichistory-service/users/{userId}/commented-articles/{commentedArticleId}",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> RemoveSearchedArticleDataAsync<TEntity>(Guid userId, Guid searchedArticleDataId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.DELETE,
                Url = $"https://localhost:7076/publichistory-service/users/{userId}/searched-articles/{searchedArticleDataId}",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> RemoveSearchedTopicDataAsync<TEntity>(Guid userId, Guid searchedTopicDataId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.DELETE,
                Url = $"https://localhost:7076/publichistory-service/users/{userId}/searched-topics/{searchedTopicDataId}",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> RemoveViewedArticleAsync<TEntity>(Guid userId, Guid viewedArticleId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.DELETE,
                Url = $"https://localhost:7076/publichistory-service/users/{userId}/viewed-articles/{viewedArticleId}",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> RemoveViewedUserAsync<TEntity>(Guid userId, Guid viewedUserId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.DELETE,
                Url = $"https://localhost:7076/publichistory-service/users/{userId}/viewed-users/{viewedUserId}",
                AccessToken = accessToken
            });

            return response;
        }
    }
}
