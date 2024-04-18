
namespace UserService.Application.Dtos
{
    public class ReceivedBanByUserDto
    {
        public DateTimeOffset DateTime { get; set; }
        public UserDto ReceiverDto { get; set; }
    }
}
