using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Repositories
{
    public interface IArticleRepository
    {
        Task<Article> GetArticleByIdAsync(ArticleID articleId);
        Task AddArticleAsync(Article article);
        Task UpdateArticleAsync(Article article);
        Task DeleteArticleAsync(Article article);
    }
}
