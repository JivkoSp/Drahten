using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Domain.Repositories;
using UserService.Domain.ValueObjects;
using UserService.Infrastructure.EntityFramework.Contexts;

namespace UserService.Infrastructure.EntityFramework.Repositories
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
                .Include(x => x.IssuedUserBans)
                .Include(x => x.IssuedContactRequests)
                .Include(x => x.ReceivedContactRequests)
                .Include(x => x.AuditTrail)
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
