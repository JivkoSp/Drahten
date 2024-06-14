namespace DrahtenWeb.Dtos.PrivateHistoryService
{
    public class TopicSubscriptionDto
    {
        public Guid TopicId { get; set; }
        public string UserId { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public DateTimeOffset? RetentionUntil { get; set; }
    }
}
