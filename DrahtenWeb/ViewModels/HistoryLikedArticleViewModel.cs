using DrahtenWeb.Dtos.PrivateHistoryService;
using DrahtenWeb.Models;

namespace DrahtenWeb.ViewModels
{
    public class HistoryLikedArticleViewModel
    {
        public List<LikedArticleDto> LikedArticles { get; set; }
        public Pagination Pagination { get; set; }
    }
}
