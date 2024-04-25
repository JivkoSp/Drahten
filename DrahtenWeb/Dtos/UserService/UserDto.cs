namespace DrahtenWeb.Dtos.UserService
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string UserFullName { get; set; }
        public string UserNickName { get; set; }
        public string UserEmailAddress { get; set; }
    }
}
