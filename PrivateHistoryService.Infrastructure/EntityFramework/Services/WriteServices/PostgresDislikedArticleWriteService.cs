using PrivateHistoryService.Application.Services.WriteServices;
using PrivateHistoryService.Domain.ValueObjects;
using PrivateHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PrivateHistoryService.Infrastructure.EntityFramework.Services.WriteServices
{
    internal sealed class PostgresDislikedArticleWriteService : IDislikedArticleWriteService
    {
        private readonly WriteDbContext _writeDbContext;

        public PostgresDislikedArticleWriteService(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
        }

        public async Task AddDislikedArticleAsync(DislikedArticle dislikedArticle)
        {
            await _writeDbContext.DislikedArticles.AddAsync(dislikedArticle);

            await _writeDbContext.SaveChangesAsync();
        }
    }
}
