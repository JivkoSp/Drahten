using PrivateHistoryService.Domain.Exceptions;

namespace PrivateHistoryService.Domain.ValueObjects
{
    public record ViewedUser
    {
        public UserID ViewerUserID { get; }
        public UserID ViewedUserID { get; }
        internal DateTimeOffset DateTime { get; }

        private ViewedUser() {}

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

            ViewerUserID = viewerUserID;
            ViewedUserID = viewedUserID;
            DateTime = dateTime;
        }
    }
}
