using PrivateHistoryService.Application.Dtos;

namespace PrivateHistoryService.Application.Services.ReadServices
{
    public interface IViewedUserReadService
    {
        Task<ViewedUserDto> GetViewedUserByIdAsync(Guid viewedUserId);
    }
}
