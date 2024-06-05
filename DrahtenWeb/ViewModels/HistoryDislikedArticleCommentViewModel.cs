using DrahtenWeb.Dtos.PrivateHistoryService;
using DrahtenWeb.Models;

namespace DrahtenWeb.ViewModels
{
    public class HistoryDislikedArticleCommentViewModel
    {
        public List<DislikedArticleCommentDto> DislikedArticleComments { get; set; }
        public Pagination Pagination { get; set; }
    }
}
