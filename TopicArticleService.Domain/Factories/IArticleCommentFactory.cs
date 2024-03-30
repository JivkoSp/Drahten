using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Factories
{
    public interface IArticleCommentFactory
    {
        ArticleComment Create(ArticleCommentID articleCommentId, ArticleCommentValue commentValue, ArticleCommentDateTime dateTime, 
                UserID userId, ArticleCommentID parentCommentId);
    }
}
