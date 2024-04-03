using Microsoft.EntityFrameworkCore;
using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.Repositories;
using TopicArticleService.Domain.ValueObjects;
using TopicArticleService.Infrastructure.EntityFramework.Contexts;

namespace TopicArticleService.Infrastructure.EntityFramework.Repositories
{
    internal sealed class PostgresArticleCommentRepository : IArticleCommentRepository
    {
        private readonly WriteDbContext _writeDbContext;

        public PostgresArticleCommentRepository(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
        }

        public Task<ArticleComment> GetArticleCommentByIdAsync(ArticleCommentID articleCommentId)
            => _writeDbContext.ArticleComments
                .Include(x => x.ArticleCommentLikes)
                .Include(x => x.ArticleCommentDislikes)
                .SingleOrDefaultAsync(x => x.Id == articleCommentId);

        public async Task UpdateArticleCommentAsync(ArticleComment articleComment)
        {
            _writeDbContext.ArticleComments.Update(articleComment);

            await _writeDbContext.SaveChangesAsync();
        }
    }
}
