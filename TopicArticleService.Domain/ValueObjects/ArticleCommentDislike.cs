using TopicArticleService.Domain.Exceptions;

namespace TopicArticleService.Domain.ValueObjects
{
    public record ArticleCommentDislike
    {
        public ArticleCommentID ArticleCommentID { get; }
        public UserID UserID { get; }
        public DateTime DateTime { get; }

        private ArticleCommentDislike()
        {
        }

        public ArticleCommentDislike(ArticleCommentID articleCommentId, UserID userId, string dateTimeString)
        {
            if (userId == null)
            {
                throw new NullArticleCommentDislikeUserIdException();
            }

            if (!DateTime.TryParse(dateTimeString, out DateTime dateTime) || dateTime == default || dateTime > DateTime.Now)
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
            return new ArticleCommentDislike(Guid.Parse(splitArticleCommentDislike[0]), Guid.Parse(splitArticleCommentDislike[1]), splitArticleCommentDislike[2]);
        }
    }
}
