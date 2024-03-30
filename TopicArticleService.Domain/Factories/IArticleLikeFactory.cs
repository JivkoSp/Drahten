using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Factories
{
    public interface IArticleLikeFactory
    {
        ArticleLike Create(ArticleID articleId, UserID userId, string dateTime);
    }
}
