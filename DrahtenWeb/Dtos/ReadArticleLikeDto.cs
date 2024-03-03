namespace DrahtenWeb.Dtos
{
    public class ReadArticleLikeDto
    {
        public DateTime DateTime { get; set; }
        public string ArticleId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
}
