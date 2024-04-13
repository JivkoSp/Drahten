using UserService.Domain.Entities;
using UserService.Domain.ValueObjects;

namespace UserService.Domain.Events
{
    public record BannedUserAdded(User User, BannedUser BannedUser) : IDomainEvent;
}
