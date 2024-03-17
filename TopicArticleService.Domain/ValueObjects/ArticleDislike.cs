using TopicArticleService.Domain.Exceptions;

namespace TopicArticleService.Domain.ValueObjects
{
    public record ArticleDislike
    {
        public DateTime DateTime { get; }
        public UserId UserId { get; }

        public ArticleDislike(string dateTimeString, UserId userId)
        {
            if (!DateTime.TryParse(dateTimeString, out DateTime dateTime) || dateTime == default || dateTime > DateTime.Now)
            {
                throw new InvalidArticleDislikeDateTimeException();
            }

            if (userId == null)
            {
                throw new NullArticleDislikeUserIdException();
            }

            DateTime = dateTime;
            UserId = userId;
        }

        //Conversion from ValueObject to String.
        public override string ToString()
            => $"{DateTime}, {UserId}";

        //Conversion from String to ValueObject.
        public static ArticleDislike Create(string value)
        {
            var splitArticleDislike = value.Split(',');
            return new ArticleDislike(splitArticleDislike[0], splitArticleDislike[1]);
        }
    }
}
