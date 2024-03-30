using Microsoft.EntityFrameworkCore;
using TopicArticleService.Application.Services.ReadServices;
using TopicArticleService.Infrastructure.EntityFramework.Contexts;

namespace TopicArticleService.Infrastructure.EntityFramework.Services
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
