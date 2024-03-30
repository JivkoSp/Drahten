using TopicArticleService.Application.Dtos;

namespace TopicArticleService.Application.Queries
{
    public class GetUsersRelatedToArticleQuery : IQuery<List<UserArticleDto>>
    {
        public string ArticleId { get; set; }
    }
}
