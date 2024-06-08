using DrahtenWeb.Models;

namespace DrahtenWeb.ViewModels
{
    public class HistoryLikedArticleCommentViewModel
    {
        public List<LikedArticleCommentViewModel> LikedArticleComments { get; set; } = new List<LikedArticleCommentViewModel>();
        public Pagination Pagination { get; set; }
    }
}
