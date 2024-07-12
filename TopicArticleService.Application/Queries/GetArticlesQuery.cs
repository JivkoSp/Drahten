using TopicArticleService.Application.Dtos;

namespace TopicArticleService.Application.Queries
{
    public record GetArticlesQuery : IQuery<List<ArticleDto>>;
}
