using UserService.Domain.Exceptions;

namespace UserService.Domain.ValueObjects
{
    public record ContactRequest
    {
        public UserID IssuerUserId { get; }
        public UserID ReceiverUserId { get; }
        internal string Message { get; }
        internal DateTimeOffset DateTime { get; }

        public ContactRequest(UserID issuerUserId, UserID receiverUserId, DateTimeOffset dateTime, string message)
        {
            if (issuerUserId == null)
            {
                throw new EmptyUserIdException();
            }

            if (receiverUserId == null)
            {
                throw new EmptyUserIdException();
            }

            if (dateTime == default || dateTime > DateTimeOffset.Now)
            {
                throw new InvalidContactRequestDateTimeException();
            }

            IssuerUserId = issuerUserId;
            ReceiverUserId = receiverUserId;
            Message = message;
            DateTime = dateTime;
        }
    }
}
