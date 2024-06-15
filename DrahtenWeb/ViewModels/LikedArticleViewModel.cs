using DrahtenWeb.Dtos.TopicArticleService;

namespace DrahtenWeb.ViewModels
{
    public class LikedArticleViewModel
    {
        public ArticleDto Article { get; set; }
        public string UserId { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public DateTimeOffset? RetentionUntil { get; set; }
    }
}
