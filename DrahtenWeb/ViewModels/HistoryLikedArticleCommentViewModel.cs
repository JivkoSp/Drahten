using DrahtenWeb.Dtos.PrivateHistoryService;
using DrahtenWeb.Models;

namespace DrahtenWeb.ViewModels
{
    public class HistoryLikedArticleCommentViewModel
    {
        public List<LikedArticleCommentDto> LikedArticleComments { get; set; }
        public Pagination Pagination { get; set; }
    }
}
