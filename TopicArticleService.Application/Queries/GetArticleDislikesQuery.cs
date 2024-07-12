using TopicArticleService.Application.Dtos;

namespace TopicArticleService.Application.Queries
{
    public record GetArticleDislikesQuery(string ArticleId) : IQuery<List<ArticleDislikeDto>>;
}
