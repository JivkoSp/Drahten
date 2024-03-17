using TopicArticleService.Domain.Exceptions;

namespace TopicArticleService.Domain.ValueObjects
{
    public record ArticleCommentValue
    {
        public string Value { get; }

        public ArticleCommentValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyArticleCommentValueException();
            }

            Value = value;
        }

        //Conversion from ValueObject to String.
        public static implicit operator string(ArticleCommentValue comment)
            => comment.Value;

        //Conversion from String to ValueObject.
        public static implicit operator ArticleCommentValue(string comment)
            => new ArticleCommentValue(comment);
    }
}
