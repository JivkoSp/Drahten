
namespace PrivateHistoryService.Application.Dtos
{
    public class ViewedArticleDto
    {
        public string ArticleId { get; set; }
        public string UserId { get; set; }
        public DateTimeOffset DateTime { get; set; }
    }
}
