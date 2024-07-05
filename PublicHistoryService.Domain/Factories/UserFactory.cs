using PublicHistoryService.Domain.Entities;
using PublicHistoryService.Domain.Factories.Interfaces;
using PublicHistoryService.Domain.ValueObjects;

namespace PublicHistoryService.Domain.Factories
{
    public sealed class UserFactory : IUserFactory
    {
        public User Create(UserID userId)
            => new User(userId);
    }
}
