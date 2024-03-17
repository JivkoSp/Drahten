using TopicArticleService.Domain.Exceptions;

namespace TopicArticleService.Domain.ValueObjects
{
    public record ArticleContent
    {
        public string Value { get; }

        public ArticleContent(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyArticleContentException();
            }

            Value = value;
        }

        //Conversion from ValueObject to String.
        public static implicit operator string(ArticleContent content)
            => content.Value;

        //Conversion from String to ValueObject.
        public static implicit operator ArticleContent(string content)
            => new ArticleContent(content);
    }
}
