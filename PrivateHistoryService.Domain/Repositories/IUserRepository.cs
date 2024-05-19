using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(UserID userId);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
    }
}
