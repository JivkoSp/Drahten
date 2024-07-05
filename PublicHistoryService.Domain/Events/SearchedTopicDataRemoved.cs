using PublicHistoryService.Domain.Entities;
using PublicHistoryService.Domain.ValueObjects;

namespace PublicHistoryService.Domain.Events
{
    public record SearchedTopicDataRemoved(User User, SearchedTopicData SearchedTopicData) : IDomainEvent;
}
