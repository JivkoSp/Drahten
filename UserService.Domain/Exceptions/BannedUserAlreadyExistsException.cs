
namespace UserService.Domain.Exceptions
{
    public sealed class BannedUserAlreadyExistsException : DomainException
    {
        internal BannedUserAlreadyExistsException(Guid userId, Guid bannedUserId) 
            : base(message: $"User #{userId} already has User #{bannedUserId} in his list of banned users!")
        {
        }
    }
}
