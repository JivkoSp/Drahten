using TopicArticleService.Domain.Exceptions;

namespace TopicArticleService.Domain.ValueObjects
{
    public record UserID
    {
        public Guid Value { get; }

        public UserID(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyUserIdException();
            }

            Value = value;
        }

        //Conversion from ValueObject to Guid.
        public static implicit operator Guid(UserID userId)
            => userId.Value;

        //Conversion from Guid to ValueObject.
        public static implicit operator UserID(Guid userId)
            => new UserID(userId);
    }
}
