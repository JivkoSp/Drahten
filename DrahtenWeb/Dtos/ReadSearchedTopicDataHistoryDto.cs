namespace DrahtenWeb.Dtos
{
    public class ReadSearchedTopicDataHistoryDto
    {
        public string SearchedData { get; set; } = string.Empty;
        public DateTime SearchTime { get; set; }
        public string HistoryId { get; set; } = string.Empty;
        public ReadTopicDto? Topic { get; set; }
    }
}
