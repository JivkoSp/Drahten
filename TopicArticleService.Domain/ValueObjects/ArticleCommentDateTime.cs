using TopicArticleService.Domain.Exceptions;

namespace TopicArticleService.Domain.ValueObjects
{
    public record ArticleCommentDateTime
    {
        public DateTime DateTime { get; }

        public ArticleCommentDateTime(DateTime dateTime)
        {
            if (dateTime == default || dateTime > DateTime.Now)
            {
                throw new InvalidArticleCommentDateTimeException();
            }

            DateTime = dateTime;
        }

        //Conversion from ValueObject to DateTime.
        public static implicit operator DateTime(ArticleCommentDateTime articleCommentDateTime)
            => articleCommentDateTime.DateTime;

        //Conversion from DateTime to ValueObject.
        public static implicit operator ArticleCommentDateTime(DateTime dateTime)
            => new ArticleCommentDateTime(dateTime);
    }
}
