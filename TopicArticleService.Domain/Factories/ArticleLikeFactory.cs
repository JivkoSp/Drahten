
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Factories
{
    public sealed class ArticleLikeFactory : IArticleLikeFactory
    {
        public ArticleLike Create(ArticleID articleId, UserID userId, string dateTime)
            => new ArticleLike(articleId, userId, dateTime);
    }
}
