
namespace PublicHistoryService.Domain.Exceptions
{
    public sealed class NullUserIdException : DomainException
    {
        internal NullUserIdException() : base(message: "User id cannot be null!")
        {
        }
    }
}
