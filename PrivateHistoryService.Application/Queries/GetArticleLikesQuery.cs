using PrivateHistoryService.Application.Dtos;

namespace PrivateHistoryService.Application.Queries
{
    public record GetArticleLikesQuery(Guid UserId) : IQuery<List<LikedArticleDto>>;
}
