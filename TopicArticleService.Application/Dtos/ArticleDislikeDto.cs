
namespace TopicArticleService.Application.Dtos
{
    public class ArticleDislikeDto
    {
        public Guid ArticleId { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public Guid UserId { get; set; }
    }
}
