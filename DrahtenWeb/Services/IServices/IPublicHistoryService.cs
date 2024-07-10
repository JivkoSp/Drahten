using DrahtenWeb.Dtos.PrivateHistoryService;

namespace DrahtenWeb.Services.IServices
{
    public interface IPublicHistoryService : IBaseService
    {
        Task<TEntity> GetCommentedArticlesAsync<TEntity>(Guid userId, string accessToken);
        Task<TEntity> GetSearchedArticlesAsync<TEntity>(Guid userId, string accessToken);
        Task<TEntity> GetSearchedTopicsAsync<TEntity>(Guid userId, string accessToken);
        Task<TEntity> GetViewedArticlesAsync<TEntity>(Guid userId, string accessToken);
        Task<TEntity> GetViewedUsersAsync<TEntity>(Guid userId, string accessToken);
        Task<TEntity> RemoveCommentedArticleAsync<TEntity>(Guid userId, Guid commentedArticleId, string accessToken);
        Task<TEntity> RemoveSearchedArticleDataAsync<TEntity>(Guid userId, Guid searchedArticleDataId, string accessToken);
        Task<TEntity> RemoveSearchedTopicDataAsync<TEntity>(Guid userId, Guid searchedTopicDataId, string accessToken);
        Task<TEntity> RemoveViewedArticleAsync<TEntity>(Guid userId, Guid viewedArticleId, string accessToken);
        Task<TEntity> RemoveViewedUserAsync<TEntity>(Guid userId, Guid viewedUserId, string accessToken);
    }
}
