using PublicHistoryService.Domain.Entities;
using PublicHistoryService.Domain.ValueObjects;

namespace PublicHistoryService.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(UserID userId);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
    }
}
