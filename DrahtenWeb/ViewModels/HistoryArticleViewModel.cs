using DrahtenWeb.Dtos.PrivateHistoryService;
using DrahtenWeb.Models;

namespace DrahtenWeb.ViewModels
{
    public class HistoryArticleViewModel
    {
        public List<ViewedArticleDto> Articles { get; set; }
        public Pagination Pagination { get; set; }
    }
}
