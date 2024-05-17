using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Domain.Events
{
    public record SearchedTopicDataRemoved(User User, SearchedTopicData SearchedTopicData) : IDomainEvent;
}
