using PublicHistoryService.Domain.Entities;
using PublicHistoryService.Domain.ValueObjects;

namespace PublicHistoryService.Domain.Events
{
    public record ViewedUserRemoved(User User, ViewedUser ViewedUser) : IDomainEvent;
}
