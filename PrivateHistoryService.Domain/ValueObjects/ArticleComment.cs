
using PrivateHistoryService.Domain.Exceptions;

namespace PrivateHistoryService.Domain.ValueObjects
{
    public record ArticleComment
    {
        internal string Value { get; }

        public ArticleComment(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyArticleCommentException();
            }

            Value = value;
        }

        //Conversion from ValueObject to String.
        public static implicit operator string(ArticleComment comment)
            => comment.Value;

        //Conversion from String to ValueObject.
        public static implicit operator ArticleComment(string comment)
            => new ArticleComment(comment);
    }
}
