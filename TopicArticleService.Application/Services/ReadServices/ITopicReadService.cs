using TopicArticleService.Application.Dtos;

namespace TopicArticleService.Application.Services.ReadServices
{
    public interface ITopicReadService
    {
        Task<bool> ExistsByIdAsync(Guid id);
        Task<TopicDto> GetTopicByNameAsync(string topicName);
    }
}
