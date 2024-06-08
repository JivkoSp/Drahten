using DrahtenWeb.Models;

namespace DrahtenWeb.ViewModels
{
    public class HistoryLikedArticleViewModel
    {
        public List<LikedArticleViewModel> LikedArticles { get; set; } = new List<LikedArticleViewModel>();
        public Pagination Pagination { get; set; }
    }
}
