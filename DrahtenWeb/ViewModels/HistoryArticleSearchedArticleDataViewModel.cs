using DrahtenWeb.Dtos.PrivateHistoryService;
using DrahtenWeb.Dtos.TopicArticleService;

namespace DrahtenWeb.ViewModels
{
    public class HistoryArticleSearchedArticleDataViewModel
    {
        public ArticleDto Article { get; set; }
        public List<SearchedArticleDataDto> SearchedArticleData { get; set; }
    }
}
