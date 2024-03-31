
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Factories
{
    public interface IArticleCommentDislikeFactory
    {
        ArticleCommentDislike Create(ArticleCommentID articleCommentId, UserID userId, string dateTimeString);
    }
}
