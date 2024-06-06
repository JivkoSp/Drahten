
namespace TopicArticleService.Application.Dtos.PrivateHistoryService
{
    public class LikedArticleDto
    {
        public string ArticleId { get; set; }
        public string UserId { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public string Event { get; set; }
    }
}
