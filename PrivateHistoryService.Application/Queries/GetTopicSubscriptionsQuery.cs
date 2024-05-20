using PrivateHistoryService.Application.Dtos;

namespace PrivateHistoryService.Application.Queries
{
    public record GetTopicSubscriptionsQuery(Guid UserId) : IQuery<List<TopicSubscriptionDto>>;
}
