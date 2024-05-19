
namespace PrivateHistoryService.Application.Exceptions
{
    public sealed class UserNotFoundException : ApplicationException
    {
        internal UserNotFoundException(Guid userId) : base(message: $"User #{userId} was NOT found!") {}
    }
}
