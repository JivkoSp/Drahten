using TopicArticleService.Domain.Exceptions;

namespace TopicArticleService.Domain.ValueObjects
{
    public record UserId
    {
        public string Value { get; }

        public UserId(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyUserIdException();
            }

            Value = value;
        }

        //Conversion from ValueObject to String.
        public static implicit operator string(UserId userId)
            => userId.Value;

        //Conversion from String to ValueObject.
        public static implicit operator UserId(string userId)
            => new UserId(userId);
    }
}
