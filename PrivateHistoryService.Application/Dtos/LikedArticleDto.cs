
namespace PrivateHistoryService.Application.Dtos
{
    public class LikedArticleDto
    {
        public Guid ArticleId { get; set; }
        public Guid UserId { get; set; }
        public DateTimeOffset DateTime { get; set; }
    }
}
