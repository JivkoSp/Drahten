using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Factories
{
    public interface IUserFactory
    {
        User Create(UserID userId);
    }
}
