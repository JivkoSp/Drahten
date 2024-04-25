namespace DrahtenWeb.Dtos.TopicArticleService
{
    public class WriteUserTopicDto
    {
        public Guid UserId { get; set; }
        public Guid TopicId { get; set; }
        public DateTimeOffset DateTime { get; set; }
    }
}
