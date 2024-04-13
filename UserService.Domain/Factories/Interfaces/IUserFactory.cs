using UserService.Domain.Entities;
using UserService.Domain.ValueObjects;

namespace UserService.Domain.Factories.Interfaces
{
    public interface IUserFactory
    {
        User Create(UserID userId, UserFullName userFullName, UserNickName userNickName, UserEmailAddress userEmailAddress);
    }
}
