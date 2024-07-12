using TopicArticleService.Application.Dtos;

namespace TopicArticleService.Application.Queries
{
    public record GetUserArticlesQuery(string UserId) : IQuery<List<ArticleDto>>;
}
