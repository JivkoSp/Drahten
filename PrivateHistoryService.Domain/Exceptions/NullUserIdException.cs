
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class NullUserIdException : DomainException
    {
        public NullUserIdException() : base(message: "User id cannot be null!")
        {
        }
    }
}
