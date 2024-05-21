using PrivateHistoryService.Application.Dtos;

namespace PrivateHistoryService.Application.Queries
{
    public record GetArticleCommentLikesQuery(Guid UserId) : IQuery<List<LikedArticleCommentDto>>;
}
