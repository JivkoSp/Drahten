using PrivateHistoryService.Domain.Exceptions;

namespace PrivateHistoryService.Domain.ValueObjects
{
    public record TopicID
    {
        public Guid Value { get; }

        public TopicID(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyTopicIdException();
            }

            Value = value;
        }

        //Conversion from ValueObject to Guid.
        public static implicit operator Guid(TopicID topicId)
            => topicId.Value;

        //Conversion from Guid to ValueObject.
        public static implicit operator TopicID(Guid topicId)
            => new TopicID(topicId);
    }
}
