namespace DrahtenWeb.Services.IServices
{
    public interface IPrivateHistoryService : IBaseService
    {
        Task<TEntity> GetCommentedArticlesAsync<TEntity>(Guid userId, string accessToken);
        Task<TEntity> GetLikedArticlesAsync<TEntity>(Guid userId, string accessToken);
        Task<TEntity> GetDislikedArticlesAsync<TEntity>(Guid userId, string accessToken);
        Task<TEntity> GetLikedArticleCommentsAsync<TEntity>(Guid userId, string accessToken);
        Task<TEntity> GetDislikedArticleCommentsAsync<TEntity>(Guid userId, string accessToken);
        Task<TEntity> GetSearchedArticlesAsync<TEntity>(Guid userId, string accessToken);
        Task<TEntity> GetSearchedTopicsAsync<TEntity>(Guid userId, string accessToken);
        Task<TEntity> GetTopicSubscriptionsAsync<TEntity>(Guid userId, string accessToken);
        Task<TEntity> GetViewedArticlesAsync<TEntity>(Guid userId, string accessToken);
        Task<TEntity> GetViewedUsersAsync<TEntity>(Guid userId, string accessToken);
        Task<TEntity> RemoveCommentedArticleAsync<TEntity>(Guid userId, Guid commentedArticleId, string accessToken);
        Task<TEntity> RemoveSearchedArticleDataAsync<TEntity>(Guid userId, Guid searchedArticleDataId, string accessToken);
        Task<TEntity> RemoveSearchedTopicDataAsync<TEntity>(Guid userId, Guid searchedTopicDataId, string accessToken);
        Task<TEntity> RemoveTopicSubscriptionAsync<TEntity>(Guid userId, Guid topicId, string accessToken);
        Task<TEntity> RemoveViewedArticleAsync<TEntity>(Guid userId, Guid viewedArticleId, string accessToken);
        Task<TEntity> RemoveViewedUserAsync<TEntity>(Guid userId, Guid viewedUserId, string accessToken);
    }
}
