using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Application.Services.WriteServices
{
    public interface ICommentedArticleWriteService
    {
        Task AddCommentedArticleAsync(CommentedArticle commentedArticle);
    }
}
