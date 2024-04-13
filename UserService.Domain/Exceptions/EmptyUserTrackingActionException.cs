
namespace UserService.Domain.Exceptions
{
    public sealed class EmptyUserTrackingActionException : DomainException
    {
        internal EmptyUserTrackingActionException() : base(message: "User tracking action cannot be empty!")
        {
        }
    }
}
