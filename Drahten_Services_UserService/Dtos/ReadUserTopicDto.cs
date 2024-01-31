namespace Drahten_Services_UserService.Dtos
{
    public class ReadUserTopicDto
    {
        public string UserId { get; set; } = string.Empty;
        public int TopicId { get; set; }
        public string? TopicName { get; set; }
        public DateTime SubscriptionTime { get; set; }
    }
}
