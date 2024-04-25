namespace DrahtenWeb.Dtos.TopicArticleService
{
    public class WriteArticleCommentDto
    {
        public Guid ArticleId { get; set; }
        public Guid ArticleCommentId { get; set; }  
        public string CommentValue { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public Guid UserId { get; set; }
        public Guid? ParentArticleCommentId { get; set; } = null;
    }
}
