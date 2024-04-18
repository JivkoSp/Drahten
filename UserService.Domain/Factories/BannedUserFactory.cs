using UserService.Domain.Factories.Interfaces;
using UserService.Domain.ValueObjects;

namespace UserService.Domain.Factories
{
    public sealed class BannedUserFactory : IBannedUserFactory
    {
        public BannedUser Create(UserID issuerUserId, UserID receiverUserId, DateTimeOffset dateTime)
            => new BannedUser(issuerUserId, receiverUserId, dateTime);
    }
}
