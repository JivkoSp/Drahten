namespace Drahten_Services_UserService.Dtos
{
    public class WriteArticleCommentDto
    {
        public int ArticleCommentId { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
        public string ArticleId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public int? ThumbsUp { get; set; }
        public int? ThumbsDown { get; set; }
        public int? ParentArticleCommentId { get; set; }
    }
}
