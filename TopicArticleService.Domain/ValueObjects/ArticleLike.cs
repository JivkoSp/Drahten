using TopicArticleService.Domain.Exceptions;

namespace TopicArticleService.Domain.ValueObjects
{
    public record ArticleLike
    {
        public DateTime DateTime { get; }
        public UserId UserId { get; }

        public ArticleLike(string dateTimeString, UserId userId)
        {
            if (!DateTime.TryParse(dateTimeString, out DateTime dateTime) || dateTime == default || dateTime > DateTime.Now)
            {
                throw new InvalidArticleLikeDateTimeException();
            }

            if (userId == null)
            {
                throw new NullArticleLikeUserIdException();
            }

            DateTime = dateTime;
            UserId = userId;
        }

        //Conversion from ValueObject to String.
        public override string ToString()
            => $"{DateTime}, {UserId}";

        //Conversion from String to ValueObject.
        public static ArticleLike Create(string value)
        {
            var splitArticleLike = value.Split(',');
            return new ArticleLike(splitArticleLike[0], splitArticleLike[1]);
        }
    }
}
