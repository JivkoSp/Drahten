using TopicArticleService.Domain.Exceptions;

namespace TopicArticleService.Domain.ValueObjects
{
    public record ArticlePublishingDate
    {
        internal string Value { get; }

        public ArticlePublishingDate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
               throw new EmptyArticlePublishingDateException();
            }

            Value = value;
        }

        //Conversion from ValueObject to String.
        public static implicit operator string(ArticlePublishingDate publishingDate)
            => publishingDate.Value;

        //Conversion from String to ValueObject.
        public static implicit operator ArticlePublishingDate(string publishingDate)
            => new ArticlePublishingDate(publishingDate);
    }
}
