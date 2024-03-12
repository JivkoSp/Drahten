namespace Drahten_Services_UserService.Dtos
{
    public class WritePrivateHistoryDto
    {
        public string PrivateHistoryId { get; set; } = string.Empty;
        public DateTime HistoryLiveTime { get; set; }
    }
}
