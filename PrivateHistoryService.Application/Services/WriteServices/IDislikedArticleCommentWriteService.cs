
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Application.Services.WriteServices
{
    public interface IDislikedArticleCommentWriteService
    {
        Task AddDislikedArticleCommentAsync(DislikedArticleComment dislikedArticleComment);
    }
}
