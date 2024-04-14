using UserService.Domain.Entities;
using UserService.Domain.ValueObjects;

namespace UserService.Domain.Events
{
    public record ContactRequestRemoved(User User, ContactRequest ContactRequest) : IDomainEvent;
}
