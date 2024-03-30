using TopicArticleService.Domain.Exceptions;

namespace TopicArticleService.Domain.ValueObjects
{
    public record TopicId
    {
        public Guid Value { get; }

        private TopicId()
        {
        }

        public TopicId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyTopicIdException();
            }

            Value = value;
        }

        //Conversion from ValueObject to Guid.
        public static implicit operator Guid(TopicId id)
            => id.Value;

        //Conversion from Guid to ValueObject.
        public static implicit operator TopicId(Guid id)
            => new TopicId(id);
    }
}
