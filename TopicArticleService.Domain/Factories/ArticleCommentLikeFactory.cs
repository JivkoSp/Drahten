
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Factories
{
    public sealed class ArticleCommentLikeFactory : IArticleCommentLikeFactory
    {
        public ArticleCommentLike Create(ArticleCommentID articleCommentId, UserID userId, DateTimeOffset dateTimeString)
            => new ArticleCommentLike(articleCommentId, userId, dateTimeString);
    }
}
