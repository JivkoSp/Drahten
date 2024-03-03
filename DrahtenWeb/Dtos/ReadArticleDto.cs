namespace DrahtenWeb.Dtos
{
    public class ReadArticleDto
    {
        public string ArticleId { get; set; } = string.Empty;
        public string PrevTitle { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Data { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public int TopicId { get; set; }
    }
}
