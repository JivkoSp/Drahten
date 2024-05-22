using Microsoft.EntityFrameworkCore;
using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Domain.Repositories;
using PrivateHistoryService.Domain.ValueObjects;
using PrivateHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PrivateHistoryService.Infrastructure.EntityFramework.Repositories
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
               .Include(x => x.SubscribedTopics)
               .Include(x => x.SearchedArticleInformation)
               .Include(x => x.SearchedTopicInformation)
               .Include(x => x.CommentedArticles)
               .Include(x => x.LikedArticles)
               .Include(x => x.DislikedArticles)
               .Include(x => x.LikedArticleComments)
               .Include(x => x.DislikedArticleComments)
               .Include(x => x.ViewedUsers)
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
