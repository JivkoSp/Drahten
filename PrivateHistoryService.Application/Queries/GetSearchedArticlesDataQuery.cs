using PrivateHistoryService.Application.Dtos;

namespace PrivateHistoryService.Application.Queries
{
    public record GetSearchedArticlesDataQuery : IQuery<List<SearchedArticleDataDto>>;
}
