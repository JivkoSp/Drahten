
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class InvalidUserRetentionUntilDateTimeException : DomainException
    {
        internal InvalidUserRetentionUntilDateTimeException() : base(message: "Invalid datetime for user retention!")
        {
        }
    }
}
