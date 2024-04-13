
namespace UserService.Domain.Exceptions
{
    public sealed class ContactRequestAlreadyExistsException : DomainException
    {
        internal ContactRequestAlreadyExistsException(Guid userId, Guid contactRequestUserId) 
            : base(message: $"User #{userId} already has User #{contactRequestUserId} in his list of contact requests!")
        {
        }
    }
}
