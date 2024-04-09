using TopicArticleService.Domain.Exceptions;

namespace TopicArticleService.Domain.ValueObjects
{
    public record ArticleCommentDateTime
    {
        public DateTimeOffset DateTime { get; }

        public ArticleCommentDateTime(DateTimeOffset dateTime)
        {
            if (dateTime == default || dateTime > DateTimeOffset.Now)
            {
                throw new InvalidArticleCommentDateTimeException();
            }

            DateTime = dateTime;
        }

        //Conversion from ValueObject to DateTimeOffset.
        public static implicit operator DateTimeOffset(ArticleCommentDateTime articleCommentDateTime)
            => articleCommentDateTime.DateTime;

        //Conversion from DateTimeOffset to ValueObject.
        public static implicit operator ArticleCommentDateTime(DateTimeOffset dateTime)
            => new ArticleCommentDateTime(dateTime);
    }
}
