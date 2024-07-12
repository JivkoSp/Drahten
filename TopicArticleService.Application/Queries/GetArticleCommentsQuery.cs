using TopicArticleService.Application.Dtos;

namespace TopicArticleService.Application.Queries
{
    public record GetArticleCommentsQuery(string ArticleId) : IQuery<List<ArticleCommentDto>>;
}
