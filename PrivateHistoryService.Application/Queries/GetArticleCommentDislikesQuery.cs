using PrivateHistoryService.Application.Dtos;

namespace PrivateHistoryService.Application.Queries
{
    public record GetArticleCommentDislikesQuery(Guid UserId) : IQuery<List<DislikedArticleCommentDto>>;
}
