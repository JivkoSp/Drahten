
namespace UserService.Application.Dtos
{
    public class IssuedContactRequestByUserDto
    {
        public string Message { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public UserDto IssuerDto { get; set; }
    }
}
