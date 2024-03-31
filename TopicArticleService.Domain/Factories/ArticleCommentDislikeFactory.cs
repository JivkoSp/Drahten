
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Factories
{
    public sealed class ArticleCommentDislikeFactory : IArticleCommentDislikeFactory
    {
        public ArticleCommentDislike Create(ArticleCommentID articleCommentId, UserID userId, string dateTimeString)
            => new ArticleCommentDislike(articleCommentId, userId, dateTimeString);
    }
}
