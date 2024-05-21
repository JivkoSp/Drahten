
namespace PrivateHistoryService.Application.Dtos
{
    public class ViewedUserDto
    {
        public Guid ViewerUserId { get; set; }
        public Guid ViewedUserId { get; set; }
        public DateTimeOffset DateTime { get; set; }
    }
}
