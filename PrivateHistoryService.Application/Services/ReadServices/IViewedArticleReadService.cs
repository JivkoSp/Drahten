
using PrivateHistoryService.Application.Dtos;

namespace PrivateHistoryService.Application.Services.ReadServices
{
    public interface IViewedArticleReadService
    {
        Task<ViewedArticleDto> GetViewedArticleByIdAsync(Guid viewedArticleId);
    }
}
