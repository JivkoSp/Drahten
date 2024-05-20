using PrivateHistoryService.Application.Dtos;

namespace PrivateHistoryService.Application.Queries
{
    public record GetSearchedTopicsDataQuery(Guid UserId) : IQuery<List<SearchedTopicDataDto>>;
}
