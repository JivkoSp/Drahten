using PublicHistoryService.Domain.Entities;
using PublicHistoryService.Domain.ValueObjects;

namespace PublicHistoryService.Domain.Events
{
    public record ViewedUserAdded(User User, ViewedUser ViewedUser) : IDomainEvent;
}
