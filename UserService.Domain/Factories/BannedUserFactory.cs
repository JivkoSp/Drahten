using UserService.Domain.Factories.Interfaces;
using UserService.Domain.ValueObjects;

namespace UserService.Domain.Factories
{
    public sealed class BannedUserFactory : IBannedUserFactory
    {
        public BannedUser Create(UserID userId)
            => new BannedUser(userId);
    }
}
