
namespace UserService.Domain.Exceptions
{
    public sealed class EmptyUserTrackingReferrerException : DomainException
    {
        internal EmptyUserTrackingReferrerException() : base(message: "User tracking referrer cannot be empty!")
        {
        }
    }
}
