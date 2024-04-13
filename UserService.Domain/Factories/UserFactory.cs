using UserService.Domain.Entities;
using UserService.Domain.Factories.Interfaces;
using UserService.Domain.ValueObjects;

namespace UserService.Domain.Factories
{
    public sealed class UserFactory : IUserFactory
    {
        public User Create(UserID userId, UserFullName userFullName, UserNickName userNickName, UserEmailAddress userEmailAddress)
            => new User(userId, userFullName, userNickName, userEmailAddress);
    }
}
