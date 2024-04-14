using UserService.Domain.Entities;
using UserService.Domain.ValueObjects;

namespace UserService.Domain.Events
{
    public record BannedUserRemoved(User User, BannedUser BannedUser) : IDomainEvent;
}
