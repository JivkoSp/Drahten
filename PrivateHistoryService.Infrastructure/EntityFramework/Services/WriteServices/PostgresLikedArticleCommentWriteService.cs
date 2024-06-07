using PrivateHistoryService.Application.Services.WriteServices;
using PrivateHistoryService.Domain.ValueObjects;
using PrivateHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PrivateHistoryService.Infrastructure.EntityFramework.Services.WriteServices
{
    internal sealed class PostgresLikedArticleCommentWriteService : ILikedArticleCommentWriteService
    {
        private readonly WriteDbContext _writeDbContext;

        public PostgresLikedArticleCommentWriteService(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
        }

        public async Task AddLikedArticleCommentAsync(LikedArticleComment likedArticleComment)
        {
            await _writeDbContext.LikedArticleComments.AddAsync(likedArticleComment);

            await _writeDbContext.SaveChangesAsync();
        }
    }
}
