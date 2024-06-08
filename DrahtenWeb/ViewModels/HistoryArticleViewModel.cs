using DrahtenWeb.Models;

namespace DrahtenWeb.ViewModels
{
    public class HistoryArticleViewModel
    {
        public List<ViewedArticleViewModel> Articles { get; set; } = new List<ViewedArticleViewModel>();
        public Pagination Pagination { get; set; }
    }
}
