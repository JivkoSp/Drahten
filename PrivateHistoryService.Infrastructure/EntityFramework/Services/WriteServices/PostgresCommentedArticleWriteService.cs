using PrivateHistoryService.Application.Services.WriteServices;
using PrivateHistoryService.Domain.ValueObjects;
using PrivateHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PrivateHistoryService.Infrastructure.EntityFramework.Services.WriteServices
{
    internal sealed class PostgresCommentedArticleWriteService : ICommentedArticleWriteService
    {
        private readonly WriteDbContext _writeDbContext;

        public PostgresCommentedArticleWriteService(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
        }

        public async Task AddCommentedArticleAsync(CommentedArticle commentedArticle)
        {
            await _writeDbContext.CommentedArticles.AddAsync(commentedArticle);

            await _writeDbContext.SaveChangesAsync();
        }
    }
}
