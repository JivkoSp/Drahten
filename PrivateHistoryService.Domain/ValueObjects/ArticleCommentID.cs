using PrivateHistoryService.Domain.Exceptions;

namespace PrivateHistoryService.Domain.ValueObjects
{
    public record ArticleCommentID
    {
        public Guid Value { get; }

        public ArticleCommentID(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyArticleCommentIdException();
            }

            Value = value;
        }

        //Conversion from ValueObject to Guid.
        public static implicit operator Guid(ArticleCommentID articleCommentId)
            => articleCommentId.Value;

        //Conversion from Guid to ValueObject.
        public static implicit operator ArticleCommentID(Guid articleCommentId)
            => new ArticleCommentID(articleCommentId);
    }
}
