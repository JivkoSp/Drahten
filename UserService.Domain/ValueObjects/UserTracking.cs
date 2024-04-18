using UserService.Domain.Exceptions;

namespace UserService.Domain.ValueObjects
{
    public record UserTracking
    {
        public UserID UserId { get; }
        internal string Action { get; }
        internal DateTimeOffset DateTime { get; }
        internal string Referrer { get; }

        private UserTracking()
        {
        }

        public UserTracking(UserID userId, string action, DateTimeOffset dateTime, string referrer)
        {
            if(userId == null)
            {
                throw new EmptyUserIdException();
            }

            if(action == null)
            {
                throw new EmptyUserTrackingActionException();
            }

            if (dateTime == default || dateTime > DateTimeOffset.Now)
            {
                throw new InvalidUserTrackingDateTimeException();
            }

            if(referrer == null)
            {
                throw new EmptyUserTrackingReferrerException();
            }

            UserId = userId;
            Action = action;
            DateTime = dateTime;
            Referrer = referrer;
        }
    }
}
