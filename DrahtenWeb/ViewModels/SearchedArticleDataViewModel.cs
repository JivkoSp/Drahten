using DrahtenWeb.Dtos.TopicArticleService;

namespace DrahtenWeb.ViewModels
{
    public class SearchedArticleDataViewModel
    {
        public Guid SearchedArticleDataId { get; set; }
        public ArticleDto Article { get; set; }
        public string UserId { get; set; }
        public string SearchedData { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public DateTimeOffset? RetentionUntil { get; set; }
    }
}
