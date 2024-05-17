
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class ViewedUserNotFoundException : DomainException
    {
        internal ViewedUserNotFoundException(Guid viewedUserID, Guid viewerUserID, DateTimeOffset dateTime) 
            : base(message: $"There is no viewed user #{viewedUserID} by user #{viewerUserID} on {dateTime}!")
        {
        }
    }
}
