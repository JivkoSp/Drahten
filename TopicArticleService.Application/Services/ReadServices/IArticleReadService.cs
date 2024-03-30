using TopicArticleService.Domain.Entities;

namespace TopicArticleService.Application.Services.ReadServices
{
    public interface IArticleReadService
    {
        Task<bool> ExistsByIdAsync(Guid id);
        Task<ArticleComment> FindArticleCommentByIdAsync(Guid id);
    }
}
