namespace DrahtenWeb.Dtos.PrivateHistoryService
{
    public class SearchedArticleDataDto
    {
        public Guid SearchedArticleDataId { get; set; }
        public string ArticleId { get; set; }
        public string UserId { get; set; }
        public string SearchedData { get; set; }
        public string SearchedDataAnswer { get; set; }
        public string SearchedDataAnswerContext { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public DateTimeOffset? RetentionUntil { get; set; }
    }
}
