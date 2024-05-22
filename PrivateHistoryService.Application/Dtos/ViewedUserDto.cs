
namespace PrivateHistoryService.Application.Dtos
{
    public class ViewedUserDto
    {
        public string ViewerUserId { get; set; }
        public string ViewedUserId { get; set; }
        public DateTimeOffset DateTime { get; set; }
    }
}
