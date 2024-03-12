using Drahten_Services_UserService.Models;

namespace Drahten_Services_UserService.Dtos
{
    public class ReadViewedUserHistoryDto
    {
        public DateTime DateTime { get; set; }
        public string HistoryId { get; set; } = string.Empty;
        public ReadUserDto? UserDto { get; set; }
    }
}
