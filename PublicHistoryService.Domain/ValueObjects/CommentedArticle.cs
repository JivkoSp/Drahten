using PublicHistoryService.Domain.Exceptions;

namespace PublicHistoryService.Domain.ValueObjects
{
    public record CommentedArticle
    {
        public ArticleID ArticleID { get; }
        public UserID UserID { get; }
        internal ArticleComment ArticleComment { get; }
        internal DateTimeOffset DateTime { get; }

        private CommentedArticle() { }

        public CommentedArticle(ArticleID articleId, UserID userId, ArticleComment articleComment, DateTimeOffset dateTime)
        {
            if (articleId == null)
            {
                throw new NullArticleIdException();
            }

            if (userId == null)
            {
                throw new NullUserIdException();
            }

            if (articleComment == null)
            {
                throw new NullArticleCommentException();
            }

            if (dateTime == default || dateTime > DateTimeOffset.Now)
            {
                throw new InvalidCommentedArticleDateTimeException();
            }

            ArticleID = articleId;
            UserID = userId;
            ArticleComment = articleComment;
            DateTime = dateTime;
        }
    }
}
