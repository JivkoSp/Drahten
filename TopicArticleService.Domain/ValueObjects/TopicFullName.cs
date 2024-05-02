using TopicArticleService.Domain.Exceptions;

namespace TopicArticleService.Domain.ValueObjects
{
    public record TopicFullName
    {
        public string Value { get; }

        public TopicFullName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyTopicFullNameException();
            }

            Value = value;
        }

        //Conversion from ValueObject to String.
        public static implicit operator string(TopicFullName name)
            => name.Value;

        //Conversion from String to ValueObject.
        public static implicit operator TopicFullName(string name)
            => new TopicFullName(name);
    }
}
