using PublicHistoryService.Application.Dtos;

namespace PublicHistoryService.Application.Queries
{
    public record GetSearchedTopicsDataQuery(Guid UserId) : IQuery<List<SearchedTopicDataDto>>;
}
}
