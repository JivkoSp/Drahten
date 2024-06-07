using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Application.Services.WriteServices
{
    public interface ILikedArticleCommentWriteService
    {
        Task AddLikedArticleCommentAsync(LikedArticleComment likedArticleComment);
    }
}
