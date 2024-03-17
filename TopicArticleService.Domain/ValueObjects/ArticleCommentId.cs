using TopicArticleService.Domain.Exceptions;

namespace TopicArticleService.Domain.ValueObjects
{
    public record ArticleCommentId
    {
        public Guid Value { get; }

        public ArticleCommentId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyArticleCommentIdException();
            }

            Value = value;
        }

        //Conversion from ValueObject to Guid.
        public static implicit operator Guid(ArticleCommentId id) 
            => id.Value;

        //Conversion from Guid to ValueObject.
        public static implicit operator ArticleCommentId(Guid id)
            => new ArticleCommentId(id);
    }
}
