
using PrivateHistoryService.Domain.Exceptions;

namespace PrivateHistoryService.Domain.ValueObjects
{
    public record LikedArticleComment
    {
        public ArticleID ArticleID { get; }
        public UserID UserID { get; }
        public ArticleCommentID ArticleCommentID { get; }
        internal DateTimeOffset DateTime { get; }

        private LikedArticleComment() {}

        public LikedArticleComment(ArticleID articleId, UserID userId, ArticleCommentID articleCommentId, DateTimeOffset dateTime)
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
                throw new InvalidLikedArticleCommentDateTimeException();
            }

            ArticleID = articleId;
            UserID = userId;
            ArticleCommentID = articleCommentId;
            DateTime = dateTime;
        }
    }
}
