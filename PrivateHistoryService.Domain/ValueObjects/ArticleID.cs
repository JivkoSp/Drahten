using PrivateHistoryService.Domain.Exceptions;

namespace PrivateHistoryService.Domain.ValueObjects
{
    public record ArticleID
    {
        public Guid Value { get; }

        public ArticleID(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyArticleIdException();
            }

            Value = value;
        }

        //Conversion from ValueObject to Guid.
        public static implicit operator Guid(ArticleID articleId)
            => articleId.Value;

        //Conversion from Guid to ValueObject.
        public static implicit operator ArticleID(Guid articleId)
            => new ArticleID(articleId);
    }
}
