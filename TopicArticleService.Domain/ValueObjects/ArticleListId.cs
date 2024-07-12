using TopicArticleService.Domain.Exceptions;

namespace TopicArticleService.Domain.ValueObjects
{
    public record ArticleListId
    {
        public Guid Value { get; }

        public ArticleListId(Guid value)
        {
            if (value == Guid.Empty)
            {
               throw new EmptyArticleListIdException();
            }

            Value = value;
        }

        //Conversion from ValueObject to Guid.
        public static implicit operator Guid(ArticleListId id)
            => id.Value;

        //Conversion from Guid to ValueObject.
        public static implicit operator ArticleListId(Guid id)
            => new ArticleListId(id);
    }
}
