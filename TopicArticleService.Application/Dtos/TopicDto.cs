
namespace TopicArticleService.Application.Dtos
{
    public class TopicDto
    {
        public string TopicName { get; set; }
        public Guid? ParentTopicId { get; set; }
        public List<TopicDto> Children { get; set; }
    }
}
