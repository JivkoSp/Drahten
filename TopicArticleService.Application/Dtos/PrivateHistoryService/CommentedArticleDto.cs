
namespace TopicArticleService.Application.Dtos.PrivateHistoryService
{
    public class CommentedArticleDto
    {
        public string ArticleId { get; set; }
        public string UserId { get; set; }
        public string ArticleComment { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public string Event { get; set; }
    }
}
