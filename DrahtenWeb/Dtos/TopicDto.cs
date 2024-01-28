namespace DrahtenWeb.Dtos
{
    public class TopicDto
    {
        public int TopicId { get; set; }
        public string TopicName { get; set; } = string.Empty;
        public int? ParentTopicId { get; set; }
        public ICollection<TopicDto>? Children { get; set; }
    }
}
