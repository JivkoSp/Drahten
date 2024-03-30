using Microsoft.EntityFrameworkCore;
using TopicArticleService.Application.Services.WriteServices;
using TopicArticleService.Domain.ValueObjects;
using TopicArticleService.Infrastructure.EntityFramework.Contexts;

namespace TopicArticleService.Infrastructure.EntityFramework.Services
{
    internal sealed class PostgresUserWriteService : IUserWriteService
    {
        private readonly WriteDbContext _writeDbContext;

        public PostgresUserWriteService(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
        }

        public Task AddUserAsync(Guid userId)
            => _writeDbContext.Database
                .ExecuteSqlInterpolatedAsync($"INSERT INTO \"topic-article-service\".\"User\" (\"UserId\") VALUES ({userId.ToString()})");
    }
}
