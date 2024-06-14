
namespace PrivateHistoryService.Application.Dtos
{
    public class CommentedArticleDto
    {
        public Guid CommentedArticleId { get; set; }
        public string ArticleId { get; set; }
        public string UserId { get; set; }
        public string ArticleComment { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public DateTimeOffset? RetentionUntil { get; set; }
    }
}
