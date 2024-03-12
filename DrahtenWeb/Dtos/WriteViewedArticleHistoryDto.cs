namespace DrahtenWeb.Dtos
{
    public class WriteViewedArticleHistoryDto
    {
        public string ArticleId { get; set; } = string.Empty;
        public DateTime ViewTime { get; set; }
        public string HistoryId { get; set; } = string.Empty;
    }
}
