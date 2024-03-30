using TopicArticleService.Domain.Exceptions;

namespace TopicArticleService.Domain.ValueObjects
{
    public record ArticleCommentID
    {
        public Guid Value { get; }

        private ArticleCommentID()
        {
        }

        public ArticleCommentID(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyArticleCommentIdException();
            }

            Value = value;
        }

        //Conversion from ValueObject to Guid.
        public static implicit operator Guid(ArticleCommentID id) 
            => id.Value;

        //Conversion from Guid to ValueObject.
        public static implicit operator ArticleCommentID(Guid id)
            => new ArticleCommentID(id);
    }
}
