
namespace PrivateHistoryService.Application.Dtos
{
    public class SearchedArticleDataDto
    {
        public Guid SearchedArticleDataId { get; set; }
        public string ArticleId { get; set; }
        public string UserId { get; set; }
        public string SearchedData { get; set; }
        public string SearchedDataAnswer { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public DateTimeOffset? RetentionUntil { get; set; }
    }
}
