using UserService.Domain.ValueObjects;

namespace UserService.Domain.Factories.Interfaces
{
    public interface IContactRequestFactory
    {
        ContactRequest Create(UserID userId, DateTimeOffset dateTime);
    }
}
