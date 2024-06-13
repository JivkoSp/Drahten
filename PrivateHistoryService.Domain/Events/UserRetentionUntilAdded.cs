using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Domain.Events
{
    public record UserRetentionUntilAdded(User User, UserRetentionUntil UserRetentionUntil) : IDomainEvent;
}
