using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Factories
{
    public sealed class UserFactory : IUserFactory
    {
        public User Create(UserID userId)
            => new User(userId);
    }
}
