using UserService.Domain.Exceptions;

namespace UserService.Domain.ValueObjects
{
    public record BannedUser
    {
        public UserID UserId { get; }

        public BannedUser(UserID userId)
        {
            if (userId == null)
            {
                throw new EmptyUserIdException();
            }

            UserId = userId;
        }
    }
}
