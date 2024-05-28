
namespace PrivateHistoryService.Application.Exceptions
{
    public sealed class UserAlreadyExistsException : ApplicationException
    {
        internal UserAlreadyExistsException(Guid userId) : base(message: $"User # {userId} already exists!")
        {
        }
    }
}
