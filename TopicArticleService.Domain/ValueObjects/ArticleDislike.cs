using TopicArticleService.Domain.Exceptions;

namespace TopicArticleService.Domain.ValueObjects
{
    public record ArticleDislike
    {
        public ArticleID ArticleID { get; }
        public UserID UserID { get; }
        public DateTime DateTime { get; }

        private ArticleDislike()
        {
        }

        public ArticleDislike(ArticleID articleId, UserID userId, string dateTimeString)
        {
            if (articleId == null)
            {
                throw new NullArticleDislikeArticleIdException();
            }

            if (userId == null)
            {
                throw new NullArticleDislikeUserIdException();
            }

            if (!DateTime.TryParse(dateTimeString, out DateTime dateTime) || dateTime == default || dateTime > DateTime.Now)
            {
                throw new InvalidArticleDislikeDateTimeException();
            }

            ArticleID = articleId;
            UserID = userId;
            DateTime = dateTime;
        }

        //Conversion from ValueObject to String.
        public override string ToString()
            => $"{ArticleID}, {UserID}, {DateTime}";

        //Conversion from String to ValueObject.
        public static ArticleDislike Create(string value)
        {
            var splitArticleDislike = value.Split(',');
            return new ArticleDislike(Guid.Parse(splitArticleDislike[0]), Guid.Parse(splitArticleDislike[1]), splitArticleDislike[2]);
        }
    }
}
