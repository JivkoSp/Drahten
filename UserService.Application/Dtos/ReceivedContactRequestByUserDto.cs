
namespace UserService.Application.Dtos
{
    public class ReceivedContactRequestByUserDto
    {
        public DateTimeOffset DateTime { get; set; }
        public UserDto ReceiverDto { get; set; }
    }
}
