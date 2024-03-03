namespace Drahten_Services_UserService.Dtos
{
    public class WriteArticleCommentThumbsUpDto
    {
        public int ArticleCommentId { get; set; }
        public string ArticleId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
}
