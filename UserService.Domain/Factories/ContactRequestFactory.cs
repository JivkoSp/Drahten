using UserService.Domain.Factories.Interfaces;
using UserService.Domain.ValueObjects;

namespace UserService.Domain.Factories
{
    public sealed class ContactRequestFactory : IContactRequestFactory
    {
        public ContactRequest Create(UserID issuerUserId, UserID receiverUserId, DateTimeOffset dateTime, string message)
            => new ContactRequest(issuerUserId, receiverUserId, dateTime, message);
    }
}
