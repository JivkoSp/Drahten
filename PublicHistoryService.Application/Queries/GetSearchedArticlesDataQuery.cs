using PublicHistoryService.Application.Dtos;

namespace PublicHistoryService.Application.Queries
{
    public record GetSearchedArticlesDataQuery(Guid UserId) : IQuery<List<SearchedArticleDataDto>>;
}
