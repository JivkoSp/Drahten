using DrahtenWeb.Dtos;

namespace DrahtenWeb.Services.IServices
{
    public interface ISearchService : IBaseService
    {
        Task<TEntity> GetAllDocumentsNewsCybersecurityEurope<TEntity>(string accessToken);
        Task<TEntity> GetAllMathingDocumentsNewsCybersecurityEurope<TEntity>(string query, string accessToken);
        Task<TEntity> GetDocumentSummarizationNewsCybersecurityEurope<TEntity>(string documentId, string accessToken);
        Task<TEntity> GetDocumentQuestionsNewsCybersecurityEurope<TEntity>(string documentId, string accessToken);
        Task<TEntity> SemanticSearchDocumentNewsCybersecurityEurope<TEntity>(DocumentQuestionDto documentQuestion, string accessToken);
    }
}
