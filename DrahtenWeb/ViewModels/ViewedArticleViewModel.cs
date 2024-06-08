using DrahtenWeb.Dtos.TopicArticleService;

namespace DrahtenWeb.ViewModels
{
    public class ViewedArticleViewModel
    {
        public Guid ViewedArticleId { get; set; }
        public ArticleDto Article { get; set; }
        public string UserId { get; set; }
        public DateTimeOffset DateTime { get; set; }
    }
}
