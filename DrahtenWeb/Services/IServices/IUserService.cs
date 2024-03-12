using DrahtenWeb.Dtos;

namespace DrahtenWeb.Services.IServices
{
    public interface IUserService : IBaseService
    {
        Task<TEntity> GetUserById<TEntity>(string userId, string accessToken);
        Task<TEntity> RegisterUser<TEntity>(WriteUserDto user, string accessToken);
        Task<TEntity> RegisterUserTopic<TEntity>(WriteUserTopicDto user, string accessToken);
        Task<TEntity> GetTopics<TEntity>(string accessToken);
        Task<TEntity> GetRootTopicWithChildren<TEntity>(int topicId, string accessToken);
        Task<TEntity> GetUserTopics<TEntity>(string userId, string accessToken);
        Task<TEntity> RegisterArticle<TEntity>(WriteArticleDto article, string accessToken);
        Task<TEntity> RegisterUserArticle<TEntity>(WriteUserArticleDto userArticle, string accessToken);
        Task<TEntity> GetArticle<TEntity>(string articleId, string accessToken);
        Task<TEntity> GetArticleLikes<TEntity>(string articleId, string accessToken);
        Task<TEntity> RegisterArticleLike<TEntity>(WriteArticleLikeDto articleLike, string accessToken);
        Task<TEntity> GetArticleComments<TEntity>(string articleId, string accessToken);
        Task<TEntity> RegisterArticleComment<TEntity>(WriteArticleCommentDto articleComment, string accessToken);
        Task<TEntity> RegisterArticleCommentThumbsUp<TEntity>(WriteArticleCommentThumbsUpDto articleCommentThumbsUp, string accessToken);
        Task<TEntity> RegisterArticleCommentThumbsDown<TEntity>(WriteArticleCommentThumbsDownDto articleCommentThumbsDown, string accessToken);
        Task<TEntity> GetArticleCommentThumbsUp<TEntity>(string articleId, int commentId, string accessToken);
        Task<TEntity> GetArticleCommentThumbsDown<TEntity>(string articleId, int commentId, string accessToken);
        Task<TEntity> DeleteArticleCommentThumbsUp<TEntity>(string articleId, int articleCommentId, string userId, string accessToken);
        Task<TEntity> DeleteArticleCommentThumbsDown<TEntity>(string articleId, int articleCommentId, string userId, string accessToken);
        Task<TEntity> GetUsersRelatedToArticle<TEntity>(string articleId, string accessToken);
        Task<TEntity> GetUserPrivateHistory<TEntity>(string userId, string accessToken);
        Task<TEntity> CreateUserPrivateHistory<TEntity>(WritePrivateHistoryDto writePrivateHistoryDto, string accessToken);
        Task<TEntity> GetUserPublicHistory<TEntity>(string userId, string accessToken);
        Task<TEntity> CreateUserPublicHistory<TEntity>(WritePublicHistoryDto writePublicHistoryDto, string accessToken);
        Task<TEntity> GetViewedArticlesPrivateHistory<TEntity>(string userId, string accessToken);
        Task<TEntity> RegisterViewedArticlePrivateHistory<TEntity>(WriteViewedArticleHistoryDto writeViewedArticleHistoryDto, string accessToken);
        Task<TEntity> GetSearchedTopicsDataPrivateHistory<TEntity>(string userId, string accessToken);
        Task<TEntity> RegisterSearchedTopicsDataPrivateHistory<TEntity>(WriteSearchedTopicDataHistoryDto writeSearchedTopicDataHistoryDto, string accessToken);
    }
}
