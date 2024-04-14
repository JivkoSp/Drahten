
namespace UserService.Domain.Exceptions
{
    public sealed class ContactRequestNotFoundException : DomainException
    {
        internal ContactRequestNotFoundException(Guid userId, Guid contactRequestUserId) 
            : base(message: $"No contact request for User #{contactRequestUserId} was found related to User #{userId}!")
        {
        }
    }
}
