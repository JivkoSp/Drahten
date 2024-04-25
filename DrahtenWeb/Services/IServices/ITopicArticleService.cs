using DrahtenWeb.Dtos.TopicArticleService;

namespace DrahtenWeb.Services.IServices
{
    public interface ITopicArticleService : IBaseService
    {
        Task<TEntity> GetTopicsAsync<TEntity>(string accessToken);
        Task<TEntity> GetParentTopicWithChildrenAsync<TEntity>(Guid topicId, string accessToken);
        Task<TEntity> GetTopicsRelatedToUserAsync<TEntity>(Guid userId, string accessToken);
        Task<TEntity> GetArticleByIdAsync<TEntity>(Guid articleId, string accessToken);
        Task<TEntity> GetArticleLikesAsync<TEntity>(Guid articleId, string accessToken);
        Task<TEntity> GetArticleDislikesAsync<TEntity>(Guid articleId, string accessToken);
        Task<TEntity> GetUsersRelatedToArticleAsync<TEntity>(Guid articleId, string accessToken);
        Task<TEntity> GetArticleCommentsAsync<TEntity>(Guid articleId, string accessToken);
        Task<TEntity> RegisterArticleAsync<TEntity>(WriteArticleDto writeArticleDto, string accessToken);
        Task<TEntity> RegisterArticleLikeAsync<TEntity>(ArticleLikeDto articleLikeDto, string accessToken);
        Task<TEntity> RegisterArticleDislikeAsync<TEntity>(ArticleDislikeDto articleDislikeDto, string accessToken);
        Task<TEntity> RegisterArticleCommentAsync<TEntity>(WriteArticleCommentDto writeArticleCommentDto, string accessToken);
        Task<TEntity> RegisterArticleCommentLikeAsync<TEntity>(ArticleCommentLikeDto articleCommentLikeDto, string accessToken);
        Task<TEntity> RegisterArticleCommentDislikeAsync<TEntity>(ArticleCommentDislikeDto articleCommentDislikeDto, string accessToken);
        Task<TEntity> RemoveArticleCommentAsync<TEntity>(Guid articleId, Guid articleCommentId, string accessToken);
        Task<TEntity> RegisterUserArticleAsync<TEntity>(WriteUserArticleDto userArticleDto, string accessToken);
        Task<TEntity> RegisterUserTopicAsync<TEntity>(WriteUserTopicDto writeUserTopicDto, string accessToken);
        Task<TEntity> RegisterUserAsync<TEntity>(Guid userId, string accessToken);
    }
}
