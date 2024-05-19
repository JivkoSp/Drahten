using PrivateHistoryService.Domain.Entities;

namespace PrivateHistoryService.Domain.Repositories
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
    }
}
