using PrivateHistoryService.Application.Dtos;

namespace PrivateHistoryService.Application.Queries
{
    public record GetViewedArticlesQuery(Guid UserId) : IQuery<List<ViewedArticleDto>>;
}
