using DrahtenWeb.Dtos.PrivateHistoryService;
using DrahtenWeb.Models;

namespace DrahtenWeb.ViewModels
{
    public class HistoryUserViewModel
    {
        public List<ViewedUserViewModel> ViewedUsers { get; set; } = new List<ViewedUserViewModel>();
        public Pagination Pagination { get; set; }
    }
}
