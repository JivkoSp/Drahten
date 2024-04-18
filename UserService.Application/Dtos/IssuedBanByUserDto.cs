
namespace UserService.Application.Dtos
{
    public class IssuedBanByUserDto
    {
        public DateTimeOffset DateTime { get; set; }
        public UserDto IssuerDto { get; set; }
    }
}
