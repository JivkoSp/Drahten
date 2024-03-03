namespace Drahten_Services_UserService.Dtos
{
    public class WriteUserDto
    {
        public string UserId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string NickName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
    }
}
