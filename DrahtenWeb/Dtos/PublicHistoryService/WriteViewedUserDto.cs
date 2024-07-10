namespace DrahtenWeb.Dtos.PublicHistoryService
{
    public class WriteViewedUserDto
    {
        public Guid ViewerUserId { get; set; } 
        public Guid ViewedUserId { get; set; } 
        public DateTimeOffset DateTime { get; set; }
    }
}
