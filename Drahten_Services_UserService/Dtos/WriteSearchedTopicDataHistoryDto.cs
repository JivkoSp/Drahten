namespace Drahten_Services_UserService.Dtos
{
    public class WriteSearchedTopicDataHistoryDto
    {
        public string SearchedData { get; set; } = string.Empty;
        public DateTime SearchTime { get; set; }
        public int TopicId { get; set; }
        public string HistoryId { get; set; } = string.Empty;
    }
}
