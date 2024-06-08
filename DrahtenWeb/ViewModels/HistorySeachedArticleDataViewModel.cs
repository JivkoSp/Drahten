using DrahtenWeb.Dtos.PrivateHistoryService;
using DrahtenWeb.Models;

namespace DrahtenWeb.ViewModels
{
    public class HistorySeachedArticleDataViewModel
    {
        public List<SearchedArticleDataViewModel> SearchedArticles { get; set; } = new List<SearchedArticleDataViewModel>();
        public Pagination Pagination { get; set; }
    }
}
