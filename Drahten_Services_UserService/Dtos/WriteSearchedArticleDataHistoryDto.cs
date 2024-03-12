namespace Drahten_Services_UserService.Dtos
{
    public class WriteSearchedArticleDataHistoryDto
    {
        public string SearchedData { get; set; } = string.Empty;
        public DateTime SearchTime { get; set; }
        public string ArticleId { get; set; } = string.Empty;
        public string HistoryId { get; set; } = string.Empty;
    }
}
