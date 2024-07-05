
namespace PublicHistoryService.Domain.Exceptions
{
    public sealed class ViewedUserAlreadyExistsException : DomainException
    {
        internal ViewedUserAlreadyExistsException(Guid viewedUserID, Guid viewerUserID)
            : base(message: $"There is already viewed user #{viewedUserID} by user #{viewerUserID}!")
        {
        }
    }
}
