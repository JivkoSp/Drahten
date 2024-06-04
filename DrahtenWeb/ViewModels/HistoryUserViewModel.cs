using DrahtenWeb.Dtos.PrivateHistoryService;
using DrahtenWeb.Models;

namespace DrahtenWeb.ViewModels
{
    public class HistoryUserViewModel
    {
        public List<ViewedUserDto> ViewedUsers { get; set; }
        public Pagination Pagination { get; set; }
    }
}
