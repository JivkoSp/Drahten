using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Application.Services.WriteServices
{
    public interface IViewedArticleWriteService
    {
        Task AddViewedArticleAsync(ViewedArticle viewedArticle);
    }
}
