using PublicHistoryService.Application.Dtos;

namespace PublicHistoryService.Application.Queries
{
    public record GetCommentedArticlesQuery(Guid UserId) : IQuery<List<CommentedArticleDto>>;
}
