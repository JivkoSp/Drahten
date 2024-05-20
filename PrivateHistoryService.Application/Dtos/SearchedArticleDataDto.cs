
namespace PrivateHistoryService.Application.Dtos
{
    public class SearchedArticleDataDto
    {
        public Guid ArticleId { get; set; }
        public Guid UserId { get; set; }
        public string SearchedData { get; set; }
        public DateTimeOffset DateTime { get; set; }
    }
}
