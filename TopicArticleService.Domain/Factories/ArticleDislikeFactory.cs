
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Factories
{
    public sealed class ArticleDislikeFactory : IArticleDislikeFactory
    {
        public ArticleDislike Create(ArticleID articleId, UserID userId, string dateTime)
            => new ArticleDislike(articleId, userId, dateTime);
    }
}
