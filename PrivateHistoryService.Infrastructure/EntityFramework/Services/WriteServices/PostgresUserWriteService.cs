using Microsoft.EntityFrameworkCore;
using PrivateHistoryService.Application.Services.WriteServices;
using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PrivateHistoryService.Infrastructure.EntityFramework.Services.WriteServices
{
    internal sealed class PostgresUserWriteService : IUserWriteService
    {
        private readonly WriteDbContext _writeDbContext;

        public PostgresUserWriteService(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
        }

        public async Task<List<User>> GetUsersAsync()
            => await _writeDbContext.Users.ToListAsync();

        public async Task UpdateUsersAsync(List<User> users)
        {
            foreach (var user in users)
            {
                _writeDbContext.Users.Update(user);
            }

            await _writeDbContext.SaveChangesAsync();
        }

        public async Task DeleteUsersAsync(List<User> users)
        {
            _writeDbContext.Users.RemoveRange(users);

            await _writeDbContext.SaveChangesAsync();
        }
    }
}
