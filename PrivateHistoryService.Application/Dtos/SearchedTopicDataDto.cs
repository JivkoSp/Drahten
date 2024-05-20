
namespace PrivateHistoryService.Application.Dtos
{
    public class SearchedTopicDataDto
    {
        public Guid TopicId { get; set; }
        public Guid UserId { get; set; }
        public string SearchedData { get; set; }
        public DateTimeOffset DateTime { get; set; }
    }
}
