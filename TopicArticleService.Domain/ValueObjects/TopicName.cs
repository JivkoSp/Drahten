using TopicArticleService.Domain.Exceptions;

namespace TopicArticleService.Domain.ValueObjects
{
    public record TopicName
    {
        public string Value { get; }

        public TopicName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyTopicNameException();
            }

            Value = value;
        }

        //Conversion from ValueObject to String.
        public static implicit operator string(TopicName name)
            => name.Value;

        //Conversion from String to ValueObject.
        public static implicit operator TopicName(string name)
            => new TopicName(name);
    }
}
