using PrivateHistoryService.Application.Services.WriteServices;
using PrivateHistoryService.Domain.ValueObjects;
using PrivateHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PrivateHistoryService.Infrastructure.EntityFramework.Services.WriteServices
{
    internal sealed class PostgresDislikedArticleCommentWriteService : IDislikedArticleCommentWriteService
    {
        private readonly WriteDbContext _writeDbContext;

        public PostgresDislikedArticleCommentWriteService(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
        }

        public async Task AddDislikedArticleCommentAsync(DislikedArticleComment dislikedArticleComment)
        {
            await _writeDbContext.DislikedArticleComments.AddAsync(dislikedArticleComment);

            await _writeDbContext.SaveChangesAsync();
        }
    }
}
