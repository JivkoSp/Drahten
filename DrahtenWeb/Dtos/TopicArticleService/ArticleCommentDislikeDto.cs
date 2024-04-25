namespace DrahtenWeb.Dtos.TopicArticleService
{
    public class ArticleCommentDislikeDto
    {
        public Guid ArticleCommentId { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public Guid UserId { get; set; }
    }
}
