using UserService.Domain.Exceptions;

namespace UserService.Domain.ValueObjects
{
    public record BannedUser
    {
        public UserID IssuerUserId { get; }
        public UserID ReceiverUserId { get; }
        internal DateTimeOffset DateTime { get; }

        public BannedUser(UserID issuerUserId, UserID receiverUserId, DateTimeOffset dateTime)
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
                throw new InvalidBannedUserDateTimeException();
            }

            IssuerUserId = issuerUserId;
            ReceiverUserId = receiverUserId;
            DateTime = dateTime;
        }
    }
}
