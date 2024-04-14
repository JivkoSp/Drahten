
namespace UserService.Domain.Exceptions
{
    public sealed class BannedUserNotFoundException : DomainException
    {
        internal BannedUserNotFoundException(Guid userId, Guid bannedUserId) 
            : base(message: $"No banned user #{bannedUserId} was found related to User #{userId}!")
        {
        }
    }
}
