using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Application.Services.WriteServices
{
    public interface ILikedArticleWriteService
    {
        Task AddLikedArticleAsync(LikedArticle likedArticle);
    }
}
