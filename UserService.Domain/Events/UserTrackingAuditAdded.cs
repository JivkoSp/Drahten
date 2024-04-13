using UserService.Domain.Entities;
using UserService.Domain.ValueObjects;

namespace UserService.Domain.Events
{
    public record UserTrackingAuditAdded(User User, UserTracking UserTracking) : IDomainEvent;
}
