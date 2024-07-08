using Microsoft.EntityFrameworkCore;
using PublicHistoryService.Domain.Entities;
using PublicHistoryService.Domain.Repositories;
using PublicHistoryService.Domain.ValueObjects;
using PublicHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PublicHistoryService.Infrastructure.EntityFramework.Repositories
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
               .Include(x => x.ViewedArticles)
               .Include(x => x.ViewedUsers)
               .Include(x => x.SearchedArticleInformation)
               .Include(x => x.SearchedTopicInformation)
               .Include(x => x.CommentedArticles)
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
