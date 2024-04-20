
namespace UserService.Application.Dtos
{
    public class ReceivedContactRequestByUserDto
    {
        public string Message { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public UserDto ReceiverDto { get; set; }
    }
}
