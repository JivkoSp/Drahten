
using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.Repositories;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Infrastructure.EntityFramework.Repositories
{
    internal sealed class PostgresTopicRepository : ITopicRepository
    {
        public Task AddTopicAsync(Topic topic)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTopicAsync(Topic topic)
        {
            throw new NotImplementedException();
        }

        public Task<Topic> GetTopicByIdAsync(TopicId topicId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTopicAsync(Topic topic)
        {
            throw new NotImplementedException();
        }
    }
}
