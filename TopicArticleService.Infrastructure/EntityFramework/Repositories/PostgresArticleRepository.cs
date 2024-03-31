using Microsoft.EntityFrameworkCore;
using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.Repositories;
using TopicArticleService.Domain.ValueObjects;
using TopicArticleService.Infrastructure.EntityFramework.Contexts;

namespace TopicArticleService.Infrastructure.EntityFramework.Repositories
{
    internal sealed class PostgresArticleRepository : IArticleRepository
    {
        private readonly WriteDbContext _writeDbContext;

        public PostgresArticleRepository(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
        }

        public Task<Article> GetArticleByIdAsync(ArticleID articleId)
            => _writeDbContext.Articles
                .Include(x => x.UserArticles)
                .Include(x => x.ArticleLikes)
                .Include(x => x.ArticleDislikes)
                .Include(x => x.ArticleComments)
                .ThenInclude(x => x.ArticleCommentLikes)
                .Include(x => x.ArticleComments)
                .ThenInclude(x => x.ArticleCommentDislikes)
                .SingleOrDefaultAsync(x => x.Id == articleId);

        public async Task AddArticleAsync(Article article)
        {
            await _writeDbContext.Articles.AddAsync(article);

            await _writeDbContext.SaveChangesAsync();
        }

        public async Task UpdateArticleAsync(Article article)
        {
            _writeDbContext.Articles.Update(article);

            await _writeDbContext.SaveChangesAsync();
        }

        public async Task DeleteArticleAsync(Article article)
        {
            _writeDbContext.Articles.Remove(article);

            await _writeDbContext.SaveChangesAsync();
        }
    }
}
