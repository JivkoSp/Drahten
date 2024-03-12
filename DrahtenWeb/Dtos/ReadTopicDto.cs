namespace DrahtenWeb.Dtos
{
    public class ReadTopicDto
    {
        public int TopicId { get; set; }
        public string TopicName { get; set; } = string.Empty;
        public int? ParentTopicId { get; set; }
        public ICollection<ReadTopicDto>? Children { get; set; }
    }
}
