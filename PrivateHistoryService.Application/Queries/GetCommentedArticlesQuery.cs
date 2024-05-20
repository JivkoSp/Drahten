using PrivateHistoryService.Application.Dtos;

namespace PrivateHistoryService.Application.Queries
{
    public record GetCommentedArticlesQuery(Guid UserId) : IQuery<List<CommentedArticleDto>>;
}
