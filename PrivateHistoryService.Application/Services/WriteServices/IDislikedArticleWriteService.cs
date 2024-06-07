using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Application.Services.WriteServices
{
    public interface IDislikedArticleWriteService
    {
        Task AddLikedArticleAsync(DislikedArticle dislikedArticle);
    }
}
