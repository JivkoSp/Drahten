
namespace PublicHistoryService.Domain.Exceptions
{
    public sealed class EmptyUserIdException : DomainException
    {
        internal EmptyUserIdException() : base(message: "User id cannot be empty!")
        {
        }
    }
}
