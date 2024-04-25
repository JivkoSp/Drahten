namespace DrahtenWeb.Dtos.UserService
{
    public class BanUserDto
    {
        public Guid IssuerUserId { get; set; }
        public Guid ReceiverUserId { get; set; }
        public DateTimeOffset DateTime { get; set; }
    }
}
