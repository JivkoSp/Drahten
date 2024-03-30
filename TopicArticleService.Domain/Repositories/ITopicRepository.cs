using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Repositories
{
    public interface ITopicRepository
    {
        Task<Topic> GetTopicByIdAsync(TopicId topicId);
        Task AddTopicAsync(Topic topic);
        Task UpdateTopicAsync(Topic topic);
        Task DeleteTopicAsync(Topic topic);
    }
}
