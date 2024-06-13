using PrivateHistoryService.Application.Services.WriteServices;
using PrivateHistoryService.Domain.ValueObjects;
using PrivateHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PrivateHistoryService.Infrastructure.EntityFramework.Services.WriteServices
{
    internal sealed class PostgresSearchedArticleDataWriteService : ISearchedArticleDataWriteService
    {
        private readonly WriteDbContext _writeDbContext;

        public PostgresSearchedArticleDataWriteService(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
        }

        public async Task AddSearchedArticleDataAsync(SearchedArticleData searchedArticleData)
        {
            await _writeDbContext.SearchedArticles.AddAsync(searchedArticleData);

            await _writeDbContext.SaveChangesAsync();
        }
    }
}
