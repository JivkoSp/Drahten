
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Factories
{
    public sealed class UserArticleFactory : IUserArticleFactory
    {
        public UserArticle Create(UserID userId, ArticleID articleId)
            => new UserArticle(userId, articleId);
    }
}
