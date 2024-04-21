
namespace TopicArticleService.Application.Dtos
{
    public class UserTopicDto
    {
        public string UserId { get; set; }
        public Guid TopicId { get; set; }
        public string TopicName { get; set; }
        public DateTimeOffset SubscriptionTime { get; set; }
    }
}
