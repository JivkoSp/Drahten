using TopicArticleService.Application.Dtos;

namespace TopicArticleService.Application.Queries
{
    public class GetArticleQuery : IQuery<ArticleDto>
    {
        public string ArticleId { get; set; }
    }
}
