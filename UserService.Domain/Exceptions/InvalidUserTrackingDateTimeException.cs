
namespace UserService.Domain.Exceptions
{
    public sealed class InvalidUserTrackingDateTimeException : DomainException
    {
        internal InvalidUserTrackingDateTimeException() : base(message: "Invalid user tracking datetime!")
        {
        }
    }
}
