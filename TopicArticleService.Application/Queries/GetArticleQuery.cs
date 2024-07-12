using TopicArticleService.Application.Dtos;

namespace TopicArticleService.Application.Queries
{
    public record GetArticleQuery(string ArticleId) : IQuery<ArticleDto>;
}
