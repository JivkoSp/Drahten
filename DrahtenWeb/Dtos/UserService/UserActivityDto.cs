namespace DrahtenWeb.Dtos.UserService
{
    public class UserActivityDto
    {
        public Guid UserId { get; set; }    
        public string Action { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public string Referrer { get; set; }
    }
}
