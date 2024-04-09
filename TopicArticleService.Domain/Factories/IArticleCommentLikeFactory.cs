using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Factories
{
    public interface IArticleCommentLikeFactory
    {
        ArticleCommentLike Create(ArticleCommentID articleCommentId, UserID userId, DateTimeOffset dateTimeString);
    }
}
