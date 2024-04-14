using UserService.Domain.ValueObjects;

namespace UserService.Domain.Factories.Interfaces
{
    public interface IUserTrackingFactory
    {
        UserTracking Create(UserID userId, string action, DateTimeOffset dateTime, string referrer);
    }
}
