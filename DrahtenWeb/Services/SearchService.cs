using DrahtenWeb.Dtos;
using DrahtenWeb.Models;
using DrahtenWeb.Services.IServices;
using Newtonsoft.Json;

namespace DrahtenWeb.Services
{
    public class SearchService : BaseService, ISearchService
    {
        public SearchService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {

        }

        public async Task<TEntity> GetAllDocumentsNewsCybersecurityEurope<TEntity>(string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = "https://localhost:7076/search_service/news/cybersecurity/europe/",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> GetAllMathingDocumentsNewsCybersecurityEurope<TEntity>(string query, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/search_service/news/cybersecurity/europe/{query}",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> GetDocumentSummarizationNewsCybersecurityEurope<TEntity>(string documentId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/search_service/news/cybersecurity/europe/summarization/documents/{documentId}",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> GetDocumentQuestionsNewsCybersecurityEurope<TEntity>(string documentId, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"https://localhost:7076/search_service/news/cybersecurity/europe/guestions/documents/{documentId}",
                AccessToken = accessToken
            });

            return response;
        }

        public async Task<TEntity> SemanticSearchDocumentNewsCybersecurityEurope<TEntity>(DocumentQuestionDto documentQuestion, string accessToken)
        {
            var response = await SendAsync<TEntity>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Url = $"https://localhost:7076/search_service/news/cybersecurity/europe/semantic-search/documents/data/",
                Data = documentQuestion,
                AccessToken = accessToken
            });

            return response;
        }
    }
}
