using DrahtenWeb.Models;

namespace DrahtenWeb.ViewModels
{
    public class HistoryDislikedArticleViewModel
    {
        public List<DislikedArticleViewModel> DislikedArticles { get; set; } = new List<DislikedArticleViewModel>();
        public Pagination Pagination { get; set; }
    }
}
