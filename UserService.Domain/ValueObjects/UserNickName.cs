using UserService.Domain.Exceptions;

namespace UserService.Domain.ValueObjects
{
    public record UserNickName
    {
        public string Value { get; }

        public UserNickName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyUserNickNameException();
            }

            Value = value;
        }

        //Conversion from ValueObject to String.
        public static implicit operator string(UserNickName content)
            => content.Value;

        //Conversion from String to ValueObject.
        public static implicit operator UserNickName(string content)
            => new UserNickName(content);
    }
}
