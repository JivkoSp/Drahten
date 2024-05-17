using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Domain.Events
{
    public record TopicSubscriptionRemoved(User User, TopicSubscription TopicSubscription) : IDomainEvent;
}
