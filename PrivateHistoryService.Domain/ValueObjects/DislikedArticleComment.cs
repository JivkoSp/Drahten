using PrivateHistoryService.Domain.Exceptions;

namespace PrivateHistoryService.Domain.ValueObjects
{
    public record DislikedArticleComment
    {
        public ArticleID ArticleID { get; }
        public UserID UserID { get; }
        public ArticleCommentID ArticleCommentID { get; }
        internal DateTimeOffset DateTime { get; }

        public DislikedArticleComment(ArticleID articleId, UserID userId, ArticleCommentID articleCommentId, DateTimeOffset dateTime)
        {
            if (articleId == null)
            {
                throw new NullArticleIdException();
            }

            if (userId == null)
            {
                throw new NullUserIdException();
            }

            if (articleCommentId == null)
            {
                throw new NullArticleCommentIdException();
            }

            if (dateTime == default || dateTime > DateTimeOffset.Now)
            {
                throw new InvalidDislikedArticleCommentDateTimeException();
            }

            ArticleID = articleId;
            UserID = userId;
            ArticleCommentID = articleCommentId;
            DateTime = dateTime;
        }
    }
}
