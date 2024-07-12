using TopicArticleService.Domain.Exceptions;

namespace TopicArticleService.Domain.ValueObjects
{
    public record ArticleTitle
    {
        internal string Value { get; }

        public ArticleTitle(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyArticleTitleException();
            }

            Value = value;
        }

        //Conversion from ValueObject to String.
        public static implicit operator string(ArticleTitle title)
            => title.Value;

        //Conversion from String to ValueObject.
        public static implicit operator ArticleTitle(string value)
        => new ArticleTitle(value);
    }
}
