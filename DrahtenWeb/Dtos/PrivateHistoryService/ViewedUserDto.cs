namespace DrahtenWeb.Dtos.PrivateHistoryService
{
    public class ViewedUserDto
    {
        public Guid ViewedUserReadModelId { get; set; }
        public string ViewerUserId { get; set; }
        public string ViewedUserId { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public DateTimeOffset? RetentionUntil { get; set; }
    }
}
