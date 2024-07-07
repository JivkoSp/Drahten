
namespace PublicHistoryService.Application.Dtos
{
    public class SearchedTopicDataDto
    {
        public Guid SearchedTopicDataId { get; set; }
        public Guid TopicId { get; set; }
        public string UserId { get; set; }
        public string SearchedData { get; set; }
        public DateTimeOffset DateTime { get; set; }
    }
}
