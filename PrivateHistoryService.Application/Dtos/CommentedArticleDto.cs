
namespace PrivateHistoryService.Application.Dtos
{
    public class CommentedArticleDto
    {
        public Guid ArticleId { get; set; }
        public Guid UserId { get; set; }
        public string ArticleComment { get; set; }
        public DateTimeOffset DateTime { get; set; }
    }
}
