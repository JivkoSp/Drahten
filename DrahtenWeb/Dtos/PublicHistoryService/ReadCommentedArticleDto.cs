namespace DrahtenWeb.Dtos.PublicHistoryService
{
    public class ReadCommentedArticleDto
    {
        public Guid CommentedArticleId { get; set; }
        public string ArticleId { get; set; }
        public string UserId { get; set; }
        public string ArticleComment { get; set; }
        public DateTimeOffset DateTime { get; set; }
    }
}
