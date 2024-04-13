using UserService.Domain.Factories.Interfaces;
using UserService.Domain.ValueObjects;

namespace UserService.Domain.Factories
{
    public sealed class ContactRequestFactory : IContactRequestFactory
    {
        public ContactRequest Create(UserID userId, DateTimeOffset dateTime)
            => new ContactRequest(userId, dateTime);    
    }
}
