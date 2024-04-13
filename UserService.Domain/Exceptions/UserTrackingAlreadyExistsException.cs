
namespace UserService.Domain.Exceptions
{
    public sealed class UserTrackingAlreadyExistsException : DomainException
    {
        internal UserTrackingAlreadyExistsException(Guid userId, string action) 
            : base(message: $"User #{userId} already has tracking information for the tracking action {action}!")
        {
        }
    }
}
