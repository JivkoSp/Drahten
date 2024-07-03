namespace DrahtenWeb.Dtos.TopicArticleService
{
    public class TopicDto
    {
        public Guid TopicId { get; set; }
        public string TopicName { get; set; }
        public string TopicFullName { get; set; }
        public Guid? ParentTopicId { get; set; }
        public List<TopicDto> Children { get; set; }
    }
}
