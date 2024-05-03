using TopicArticleService.Application.Dtos;

namespace TopicArticleService.Application.Queries
{
    public class GetUserArticlesQuery : IQuery<List<ArticleDto>>
    {
        public string UserId { get; set; }
    }
}
