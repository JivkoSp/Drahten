using PrivateHistoryService.Application.Services.WriteServices;
using PrivateHistoryService.Domain.ValueObjects;
using PrivateHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PrivateHistoryService.Infrastructure.EntityFramework.Services.WriteServices
{
    internal sealed class PostgresLikedArticleWriteService : ILikedArticleWriteService
    {
        private readonly WriteDbContext _writeDbContext;

        public PostgresLikedArticleWriteService(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
        }

        public async Task AddLikedArticleAsync(LikedArticle likedArticle)
        {
            await _writeDbContext.LikedArticles.AddAsync(likedArticle);

            await _writeDbContext.SaveChangesAsync();
        }
    }
}
