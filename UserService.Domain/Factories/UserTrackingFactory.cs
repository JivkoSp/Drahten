using UserService.Domain.Factories.Interfaces;
using UserService.Domain.ValueObjects;

namespace UserService.Domain.Factories
{
    public sealed class UserTrackingFactory : IUserTrackingFactory
    {
        public UserTracking Create(UserID userId, string action, DateTimeOffset dateTime, string referrer)
            => new UserTracking(userId, action, dateTime, referrer);
    }
}
