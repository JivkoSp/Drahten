using PublicHistoryService.Application.Dtos;

namespace PublicHistoryService.Application.Queries
{
    public record GetViewedArticlesQuery(Guid UserId) : IQuery<List<ViewedArticleDto>>;
}
