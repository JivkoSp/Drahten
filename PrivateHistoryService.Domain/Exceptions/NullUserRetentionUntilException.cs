
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class NullUserRetentionUntilException : DomainException
    {
        internal NullUserRetentionUntilException(Guid userId) : base(message: $"User #{userId} cannot be set with a null UserRetentionUntil value!")
        {
        }
    }
}
