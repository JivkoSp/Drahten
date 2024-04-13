using UserService.Domain.ValueObjects;

namespace UserService.Domain.Factories.Interfaces
{
    public interface IBannedUserFactory
    {
        BannedUser Create(UserID userId);
    }
}
