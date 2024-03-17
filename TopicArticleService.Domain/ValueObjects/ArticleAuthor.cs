using TopicArticleService.Domain.Exceptions;

namespace TopicArticleService.Domain.ValueObjects
{
    public record ArticleAuthor
    {
        public string Value { get; }

        public ArticleAuthor(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
              throw new EmptyArticleAuthorException();
            }

            Value = value;
        }

        //Conversion from ValueObject to String.
        public static implicit operator string(ArticleAuthor articleAuthor)
            => articleAuthor.Value;

        //Conversion from String to ValueObject.
        public static implicit operator ArticleAuthor(string articleAuthor)
            => new ArticleAuthor(articleAuthor);
    }
}
