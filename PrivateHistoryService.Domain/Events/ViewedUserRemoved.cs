using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Domain.Events
{
    public record ViewedUserRemoved(User User, ViewedUser ViewedUser) : IDomainEvent;
}
