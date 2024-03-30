using TopicArticleService.Domain.Exceptions;

namespace TopicArticleService.Domain.ValueObjects
{
    public record ArticleCommentLike
    {
        public ArticleCommentID ArticleCommentID { get; }
        public UserID UserID { get; }
        public DateTime DateTime { get; }
      
        private ArticleCommentLike()
        {
        }

        public ArticleCommentLike(ArticleCommentID articleCommentId, UserID userId, string dateTimeString)
        {
            if (userId == null)
            {
                throw new NullArticleCommentLikeUserIdException();
            }

            if (!DateTime.TryParse(dateTimeString, out DateTime dateTime) || dateTime == default || dateTime > DateTime.Now)
            {
                throw new InvalidArticleCommentLikeDateTimeException();
            }

            ArticleCommentID = articleCommentId;
            UserID = userId;
            DateTime = dateTime;
        }

        //Conversion from ValueObject to String.
        public override string ToString()
           => $"{ArticleCommentID}, {UserID}, {DateTime}";

        //Conversion from String to ValueObject.
        public static ArticleCommentLike Create(string value)
        {
            var splitArticleCommentLike = value.Split(',');
            return new ArticleCommentLike(Guid.Parse(splitArticleCommentLike[0]), Guid.Parse(splitArticleCommentLike[1]), splitArticleCommentLike[2]);
        }
    }
}
