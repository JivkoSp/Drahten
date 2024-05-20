using PrivateHistoryService.Application.Dtos;

namespace PrivateHistoryService.Application.Queries
{
    public record GetTopicSubscriptionsQuery : IQuery<List<TopicSubscriptionDto>>;
}
