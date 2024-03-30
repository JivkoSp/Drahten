using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Factories
{
    public interface IUserArticleFactory
    {
        UserArticle Create(UserID userId, ArticleID articleId);
    }
}
