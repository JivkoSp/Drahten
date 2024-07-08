using Microsoft.EntityFrameworkCore;
using PublicHistoryService.Application.Services.ReadServices;
using PublicHistoryService.Infrastructure.EntityFramework.Contexts;

namespace PublicHistoryService.Infrastructure.EntityFramework.Services.ReadServices
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
