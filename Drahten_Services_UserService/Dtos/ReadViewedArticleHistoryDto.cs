using Drahten_Services_UserService.Models;

namespace Drahten_Services_UserService.Dtos
{
    public class ReadViewedArticleHistoryDto
    {
        public DateTime ViewTime { get; set; }
        public string HistoryId { get; set; } = string.Empty;
        public ReadArticleDto? Article { get; set; }
    }
}
