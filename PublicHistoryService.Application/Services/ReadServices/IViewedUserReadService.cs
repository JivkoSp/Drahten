using PublicHistoryService.Application.Dtos;

namespace PublicHistoryService.Application.Services.ReadServices
{
    public interface IViewedUserReadService
    {
        Task<ViewedUserDto> GetViewedUserByIdAsync(Guid viewedUserId);
    }
}
