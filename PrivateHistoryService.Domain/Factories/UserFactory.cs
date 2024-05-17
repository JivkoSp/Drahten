using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Domain.Factories.Interfaces;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Domain.Factories
{
    public sealed class UserFactory : IUserFactory
    {
        public User Create(UserID userId)
            => new User(userId);
    }
}
