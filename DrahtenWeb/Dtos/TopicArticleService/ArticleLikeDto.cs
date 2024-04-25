namespace DrahtenWeb.Dtos.TopicArticleService
{
    public class ArticleLikeDto
    {
        public Guid ArticleId { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public Guid UserId { get; set; }
    }
}
