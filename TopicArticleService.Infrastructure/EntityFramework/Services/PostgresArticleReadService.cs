using Microsoft.EntityFrameworkCore;
using TopicArticleService.Application.Services.ReadServices;
using TopicArticleService.Infrastructure.EntityFramework.Contexts;

namespace TopicArticleService.Infrastructure.EntityFramework.Services
{
    internal sealed class PostgresArticleReadService : IArticleReadService
    {
        private readonly ReadDbContext _readDbContext;

        public PostgresArticleReadService(ReadDbContext readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public Task<bool> ExistsByIdAsync(Guid id)
            => _readDbContext.Articles.AnyAsync(x => x.ArticleId == id.ToString("N"));
    }
}
