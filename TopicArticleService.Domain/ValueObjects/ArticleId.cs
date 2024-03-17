using TopicArticleService.Domain.Exceptions;

namespace TopicArticleService.Domain.ValueObjects
{
    public record ArticleId
    {
        public Guid Value { get; }

        public ArticleId(Guid value)
        {
            if(value == Guid.Empty)
            {
                throw new EmptyArticleIdException();
            }

            Value = value;
        }

        //Conversion from ValueObject to Guid.
        public static implicit operator Guid(ArticleId id)
            => id.Value;

        //Conversion from Guid to ValueObject.
        public static implicit operator ArticleId(Guid id)
            => new ArticleId(id);
    }
}
