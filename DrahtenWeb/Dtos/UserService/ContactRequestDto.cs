namespace DrahtenWeb.Dtos.UserService
{
    public class ContactRequestDto
    {
        public Guid IssuerUserId { get; set; }
        public Guid ReceiverUserId { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public string Message { get; set; } = null;
    }
}
