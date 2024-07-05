using PublicHistoryService.Domain.Exceptions;

namespace PublicHistoryService.Domain.ValueObjects
{
    public record ViewedUser
    {
        public UserID ViewerUserId { get; }
        public UserID ViewedUserId { get; }
        internal DateTimeOffset DateTime { get; }

        private ViewedUser() { }

        public ViewedUser(UserID viewerUserID, UserID viewedUserID, DateTimeOffset dateTime)
        {
            if (viewerUserID == null)
            {
                throw new NullUserIdException();
            }

            if (viewedUserID == null)
            {
                throw new NullUserIdException();
            }

            if (dateTime == default || dateTime > DateTimeOffset.Now)
            {
                throw new InvalidViewedUserDateTimeException();
            }

            ViewerUserId = viewerUserID;
            ViewedUserId = viewedUserID;
            DateTime = dateTime;
        }
    }
}
