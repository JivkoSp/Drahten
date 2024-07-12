using TopicArticleService.Application.Dtos;

namespace TopicArticleService.Application.Queries
{
    public record GetArticleLikesQuery(string ArticleId) : IQuery<List<ArticleLikeDto>>;
}
