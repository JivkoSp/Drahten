using DrahtenWeb.Dtos.TopicArticleService;

namespace DrahtenWeb.ViewModels
{
    public class CommentedArticleViewModel
    {
        public Guid CommentedArticleId { get; set; }
        public ArticleDto Article { get; set; }
        public string UserId { get; set; }
        public string ArticleComment { get; set; }
        public DateTimeOffset DateTime { get; set; }
    }
}
