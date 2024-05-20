using PrivateHistoryService.Application.Dtos;

namespace PrivateHistoryService.Application.Queries
{
    public record GetSearchedArticlesDataQuery(Guid UserId) : IQuery<List<SearchedArticleDataDto>>;
}
