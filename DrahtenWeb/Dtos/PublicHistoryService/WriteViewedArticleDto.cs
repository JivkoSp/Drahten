namespace DrahtenWeb.Dtos.PublicHistoryService
{
    public class WriteViewedArticleDto
    {
        public Guid ArticleId { get; set; }
        public Guid UserId { get; set; } 
        public DateTimeOffset DateTime { get; set; }
    }
}
