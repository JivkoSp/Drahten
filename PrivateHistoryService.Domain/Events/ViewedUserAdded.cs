using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Domain.Events
{
    public record ViewedUserAdded(User User, ViewedUser ViewedUser) : IDomainEvent;
}
