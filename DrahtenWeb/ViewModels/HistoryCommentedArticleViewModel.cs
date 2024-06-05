using DrahtenWeb.Dtos.PrivateHistoryService;
using DrahtenWeb.Models;

namespace DrahtenWeb.ViewModels
{
    public class HistoryCommentedArticleViewModel
    {
        public List<CommentedArticleDto> CommentedArticles { get; set; }
        public Pagination Pagination { get; set; }
    }
}
