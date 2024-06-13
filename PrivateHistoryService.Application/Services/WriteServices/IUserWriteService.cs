using PrivateHistoryService.Domain.Entities;

namespace PrivateHistoryService.Application.Services.WriteServices
{
    public interface IUserWriteService
    {
        Task<List<User>> GetUsersAsync();
        Task UpdateUsersAsync(List<User> users);
        Task DeleteUsersAsync(List<User> users);
    }
}
