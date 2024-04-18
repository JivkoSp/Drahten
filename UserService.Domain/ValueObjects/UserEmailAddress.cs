using UserService.Domain.Exceptions;

namespace UserService.Domain.ValueObjects
{
    public record UserEmailAddress
    {
        internal string Value { get; }

        public UserEmailAddress(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyUserEmailAddressException();
            }

            Value = value;
        }

        //Conversion from ValueObject to String.
        public static implicit operator string(UserEmailAddress content)
            => content.Value;

        //Conversion from String to ValueObject.
        public static implicit operator UserEmailAddress(string content)
            => new UserEmailAddress(content);
    }
}
