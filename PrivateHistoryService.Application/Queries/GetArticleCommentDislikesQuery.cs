using PrivateHistoryService.Application.Dtos;

namespace PrivateHistoryService.Application.Queries
{
    public record GetArticleCommentDislikesQuery : IQuery<List<DislikedArticleCommentDto>>;
}
