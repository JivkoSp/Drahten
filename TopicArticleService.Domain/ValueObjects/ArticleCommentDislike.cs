using TopicArticleService.Domain.Exceptions;

namespace TopicArticleService.Domain.ValueObjects
{
    public record ArticleCommentDislike
    {
        public ArticleCommentID ArticleCommentID { get; }
        public UserID UserID { get; }
        public DateTimeOffset DateTime { get; }

        private ArticleCommentDislike()
        {
        }

        public ArticleCommentDislike(ArticleCommentID articleCommentId, UserID userId, DateTimeOffset dateTime)
        {
            if (userId == null)
            {
                throw new NullArticleCommentDislikeUserIdException();
            }

            if (dateTime == default || dateTime > DateTimeOffset.Now)
            {
                throw new InvalidArticleCommentDislikeDateTimeException();
            }
           
            ArticleCommentID = articleCommentId;
            DateTime = dateTime;
            UserID = userId;
        }

        //Conversion from ValueObject to String.
        public override string ToString()
           => $"{ArticleCommentID}, {UserID}, {DateTime}";

        //Conversion from String to ValueObject.
        public static ArticleCommentDislike Create(string value)
        {
            var splitArticleCommentDislike = value.Split(',');
            return new ArticleCommentDislike(Guid.Parse(splitArticleCommentDislike[0]), Guid.Parse(splitArticleCommentDislike[1]), DateTimeOffset.Parse(splitArticleCommentDislike[2]));
        }
    }
}
