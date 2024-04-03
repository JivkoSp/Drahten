using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Repositories
{
    public interface IArticleCommentRepository
    {
        Task<ArticleComment> GetArticleCommentByIdAsync(ArticleCommentID articleCommentId);
        Task UpdateArticleCommentAsync(ArticleComment articleComment);
    }
}
