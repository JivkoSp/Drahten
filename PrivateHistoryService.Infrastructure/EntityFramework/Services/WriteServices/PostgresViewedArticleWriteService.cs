using PrivateHistoryService.Application.Services.WriteServices;
using PrivateHistoryService.Domain.ValueObjects;
using PrivateHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PrivateHistoryService.Infrastructure.EntityFramework.Services.WriteServices
{
    internal sealed class PostgresViewedArticleWriteService : IViewedArticleWriteService
    {
        private readonly WriteDbContext _writeDbContext;

        public PostgresViewedArticleWriteService(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
        }

        public async Task AddViewedArticleAsync(ViewedArticle viewedArticle)
        {
            await _writeDbContext.ViewedArticles.AddAsync(viewedArticle);

            await _writeDbContext.SaveChangesAsync();
        }
    }
}
