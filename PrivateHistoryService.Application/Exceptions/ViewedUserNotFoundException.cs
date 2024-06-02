
namespace PrivateHistoryService.Application.Exceptions
{
    public sealed class ViewedUserNotFoundException : ApplicationException
    {
        internal ViewedUserNotFoundException(Guid viewedUserId) 
            : base(message: $"Viewed user ${viewedUserId} was NOT found!")
        {
        }
    }
}
