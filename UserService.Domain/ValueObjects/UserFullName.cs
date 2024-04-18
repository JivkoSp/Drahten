using UserService.Domain.Exceptions;

namespace UserService.Domain.ValueObjects
{
    public record UserFullName
    {
        internal string Value { get; }

        public UserFullName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyUserFullNameException();
            }

            Value = value;
        }

        //Conversion from ValueObject to String.
        public static implicit operator string(UserFullName content)
            => content.Value;

        //Conversion from String to ValueObject.
        public static implicit operator UserFullName(string content)
            => new UserFullName(content);
    }
}
