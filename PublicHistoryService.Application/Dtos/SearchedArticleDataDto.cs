
namespace PublicHistoryService.Application.Dtos
{
    public class SearchedArticleDataDto
    {
        public Guid SearchedArticleDataId { get; set; }
        public string ArticleId { get; set; }
        public string UserId { get; set; }
        public string SearchedData { get; set; }
        public DateTimeOffset DateTime { get; set; }
    }
}
