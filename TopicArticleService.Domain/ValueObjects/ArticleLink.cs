using TopicArticleService.Domain.Exceptions;

namespace TopicArticleService.Domain.ValueObjects
{
    public record ArticleLink
    {
        internal string Value { get; }

        public ArticleLink(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
               throw new EmptyArticleLinkException();
            }

            Value = value;
        }

        //Conversion from ValueObject to String.
        public static implicit operator string(ArticleLink link)
            => link.Value;

        //Conversion from String to ValueObject.
        public static implicit operator ArticleLink(string link)
            => new ArticleLink(link);
    }
}
