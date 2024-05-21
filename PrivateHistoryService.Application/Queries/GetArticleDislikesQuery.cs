using PrivateHistoryService.Application.Dtos;

namespace PrivateHistoryService.Application.Queries
{
    public record GetArticleDislikesQuery(Guid UserId) : IQuery<List<DislikedArticleDto>>;
}
