
namespace TopicArticleService.Application.Dtos
{
    public class ArticleDislikeDto
    {
        public DateTimeOffset DateTime { get; set; }
        public Guid UserId { get; set; }
    }
}
