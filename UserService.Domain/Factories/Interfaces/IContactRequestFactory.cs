using UserService.Domain.ValueObjects;

namespace UserService.Domain.Factories.Interfaces
{
    public interface IContactRequestFactory
    {
        ContactRequest Create(UserID issuerUserId, UserID receiverUserId, DateTimeOffset dateTime, string message);
    }
}
