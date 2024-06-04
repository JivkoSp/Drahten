using DrahtenWeb.Dtos.PrivateHistoryService;
using DrahtenWeb.Models;

namespace DrahtenWeb.ViewModels
{
    public class HistorySeachedArticleDataViewModel
    {
        public List<SearchedArticleDataDto> SearchedArticles { get; set; }
        public Pagination Pagination { get; set; }
    }
}
