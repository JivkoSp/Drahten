namespace Drahten_Services_UserService.Dtos
{
    public class WriteUserTopicDto
    {
        public string UserId { get; set; } = string.Empty;
        public int TopicId { get; set; }
        public DateTime SubscriptionTime { get; set; }
    }
}
