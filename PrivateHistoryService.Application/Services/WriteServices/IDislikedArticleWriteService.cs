using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Application.Services.WriteServices
{
    public interface IDislikedArticleWriteService
    {
        Task AddDislikedArticleAsync(DislikedArticle dislikedArticle);
    }
}
