using DrahtenWeb.Models;

namespace DrahtenWeb.ViewModels
{
    public class HistoryCommentedArticleViewModel
    {
        public List<CommentedArticleViewModel> CommentedArticles { get; set; } = new List<CommentedArticleViewModel>();
        public Pagination Pagination { get; set; }
    }
}
