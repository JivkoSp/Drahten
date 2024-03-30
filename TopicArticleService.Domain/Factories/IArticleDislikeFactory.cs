using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Factories
{
    public interface IArticleDislikeFactory
    {
        ArticleDislike Create(ArticleID articleId, UserID userId, string dateTime);
    }
}
