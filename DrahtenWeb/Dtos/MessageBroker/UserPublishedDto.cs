namespace DrahtenWeb.Dtos.MessageBroker
{
    public class UserPublishedDto
    {
        public Guid UserId { get; set; }
        public string UserFullName { get; set; }
        public string UserNickName { get; set; }
        public string UserEmailAddress { get; set; }
        public string Event { get; set; }
    }
}
