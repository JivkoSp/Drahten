using Drahten_Services_UserService.Models;

namespace Drahten_Services_UserService.Dtos
{
    public class ReadPrivateHistoryDto
    {
        public string PrivateHistoryId { get; set; } = string.Empty;
        public DateTime HistoryLiveTime { get; set; }
    }
}
