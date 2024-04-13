using UserService.Domain.Entities;
using UserService.Domain.ValueObjects;

namespace UserService.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(UserID userId);
        Task AddUserAsync(User user);  
        Task UpdateUserAsync(User user);
    }
}
