using DrahtenWeb.Dtos.UserService;

namespace DrahtenWeb.ViewModels
{
    public class ViewedUserViewModel
    {
        public Guid ViewedUserReadModelId { get; set; }
        public UserDto ViewedUser { get; set; }
        public DateTimeOffset DateTime { get; set; }
    }
}
