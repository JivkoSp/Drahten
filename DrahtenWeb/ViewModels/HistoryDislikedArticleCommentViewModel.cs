using DrahtenWeb.Models;

namespace DrahtenWeb.ViewModels
{
    public class HistoryDislikedArticleCommentViewModel
    {
        public List<DislikedArticleCommentViewModel> DislikedArticleComments { get; set; } = new List<DislikedArticleCommentViewModel>();
        public Pagination Pagination { get; set; }
    }
}
