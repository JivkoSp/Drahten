using Drahten_Services_UserService.Models;

namespace Drahten_Services_UserService.Dtos
{
    public class ReadSearchedTopicDataHistoryDto
    {
        public string SearchedData { get; set; } = string.Empty;
        public DateTime SearchTime { get; set; }
        public string HistoryId { get; set; } = string.Empty;
        public ReadTopicDto? Topic { get; set; }
    }
}
