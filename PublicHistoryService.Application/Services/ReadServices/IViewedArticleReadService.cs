using PublicHistoryService.Application.Dtos;

namespace PublicHistoryService.Application.Services.ReadServices
{
    public interface IViewedArticleReadService
    {
        Task<ViewedArticleDto> GetViewedArticleByIdAsync(Guid viewedArticleId);
    }
}
