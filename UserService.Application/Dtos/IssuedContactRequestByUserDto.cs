
namespace UserService.Application.Dtos
{
    public class IssuedContactRequestByUserDto
    {
        public DateTimeOffset DateTime { get; set; }
        public UserDto IssuerDto { get; set; }
    }
}
