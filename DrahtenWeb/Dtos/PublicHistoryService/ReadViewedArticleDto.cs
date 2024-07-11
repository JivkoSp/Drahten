namespace DrahtenWeb.Dtos.PublicHistoryService
{
    public class ReadViewedArticleDto
    {
        public Guid ViewedArticleId { get; set; }
        public string ArticleId { get; set; }
        public string UserId { get; set; }
        public DateTimeOffset DateTime { get; set; }
    }
}
