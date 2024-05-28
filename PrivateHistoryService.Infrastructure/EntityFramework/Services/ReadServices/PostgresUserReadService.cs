using Microsoft.EntityFrameworkCore;
using PrivateHistoryService.Application.Services.ReadServices;
using PrivateHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PrivateHistoryService.Infrastructure.EntityFramework.Services.ReadServices
{
    internal sealed class PostgresUserReadService : IUserReadService
    {
        private readonly ReadDbContext _readDbContext;

        public PostgresUserReadService(ReadDbContext readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public Task<bool> ExistsByIdAsync(Guid id)
            => _readDbContext.Users.AnyAsync(x => x.UserId == id.ToString());
    }
}
