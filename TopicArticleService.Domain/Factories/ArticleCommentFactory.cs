
using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Factories
{
    public sealed class ArticleCommentFactory : IArticleCommentFactory
    {
        public ArticleComment Create(ArticleCommentID articleCommentId, ArticleCommentValue commentValue, ArticleCommentDateTime dateTime, 
            UserID userId, ArticleCommentID parentCommentId)
            => new ArticleComment(articleCommentId, commentValue, dateTime, userId, parentCommentId);
    }
}
