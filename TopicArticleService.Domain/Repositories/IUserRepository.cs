using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(UserID userId);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
    }
}
