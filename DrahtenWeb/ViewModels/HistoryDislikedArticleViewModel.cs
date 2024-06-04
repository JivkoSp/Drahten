using DrahtenWeb.Dtos.PrivateHistoryService;
using DrahtenWeb.Models;

namespace DrahtenWeb.ViewModels
{
    public class HistoryDislikedArticleViewModel
    {
        public List<DislikedArticleDto> DislikedArticles { get; set; }
        public Pagination Pagination { get; set; }
    }
}
