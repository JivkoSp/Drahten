using Microsoft.EntityFrameworkCore;
using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.Repositories;
using TopicArticleService.Domain.ValueObjects;
using TopicArticleService.Infrastructure.EntityFramework.Contexts;

namespace TopicArticleService.Infrastructure.EntityFramework.Repositories
{
    internal sealed class PostgresTopicRepository : ITopicRepository
    {
        private readonly WriteDbContext _writeDbContext;

        public PostgresTopicRepository(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
        }

        public Task<Topic> GetTopicByIdAsync(TopicId topicId)
            => _writeDbContext.Topics
                .Include(x => x.TopicChildren)
                .SingleOrDefaultAsync(x => x.Id == topicId);

        public async Task AddTopicAsync(Topic topic)
        {
            await _writeDbContext.Topics.AddAsync(topic);

            await _writeDbContext.SaveChangesAsync();
        }
        public async Task UpdateTopicAsync(Topic topic)
        {
            _writeDbContext.Topics.Update(topic);

            await _writeDbContext.SaveChangesAsync();
        }

        public async Task DeleteTopicAsync(Topic topic)
        {
            _writeDbContext.Topics.Remove(topic);

            await _writeDbContext.SaveChangesAsync();
        }
    }
}
