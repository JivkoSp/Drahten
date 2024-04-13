using UserService.Domain.Exceptions;

namespace UserService.Domain.ValueObjects
{
    public record ContactRequest
    {
        public UserID UserId { get; }
        private DateTimeOffset _dateTime;

        public ContactRequest(UserID userId, DateTimeOffset dateTime)
        {
            if (userId == null)
            {
                throw new EmptyUserIdException();
            }

            if (dateTime == default || dateTime > DateTimeOffset.Now)
            {
                throw new InvalidContactRequestDateTimeException();
            }

            UserId = userId;
            _dateTime = dateTime;
        }
    }
}
