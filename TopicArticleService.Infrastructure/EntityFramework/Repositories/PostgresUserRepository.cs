using Microsoft.EntityFrameworkCore;
using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.Repositories;
using TopicArticleService.Domain.ValueObjects;
using TopicArticleService.Infrastructure.EntityFramework.Contexts;

namespace TopicArticleService.Infrastructure.EntityFramework.Repositories
{
    internal sealed class PostgresUserRepository : IUserRepository
    {
        private readonly WriteDbContext _writeDbContext;

        public PostgresUserRepository(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
        }

        public Task<User> GetUserByIdAsync(UserID userId)
            => _writeDbContext.Users
                .Include(x => x.SubscribedTopics)
                .SingleOrDefaultAsync(x => x.Id == userId);

        public async Task AddUserAsync(User user)
        {
            await _writeDbContext.Users.AddAsync(user);

            await _writeDbContext.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _writeDbContext.Users.Update(user);

            await _writeDbContext.SaveChangesAsync();
        }
    }
}
