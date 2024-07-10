namespace DrahtenWeb.Dtos.PublicHistoryService
{
    public class WriteSearchedTopicDataDto
    {
        public Guid TopicId { get; set; } 
        public Guid UserId { get; set; } 
        public string SearchedData { get; set; } 
        public DateTimeOffset DateTime { get; set; }
    }
}
