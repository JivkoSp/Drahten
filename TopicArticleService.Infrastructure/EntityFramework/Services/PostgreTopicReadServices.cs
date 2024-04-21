using Microsoft.EntityFrameworkCore;
using TopicArticleService.Application.Services.ReadServices;
using TopicArticleService.Infrastructure.EntityFramework.Contexts;

namespace TopicArticleService.Infrastructure.EntityFramework.Services
{
    internal sealed class PostgreTopicReadServices : ITopicReadService
    {
        private readonly ReadDbContext _readDbContext;

        public PostgreTopicReadServices(ReadDbContext readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public Task<bool> ExistsByIdAsync(Guid id)
            => _readDbContext.Topics.AnyAsync(x => x.TopicId == id);
    }
}
