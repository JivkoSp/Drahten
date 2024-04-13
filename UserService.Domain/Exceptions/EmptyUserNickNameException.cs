
namespace UserService.Domain.Exceptions
{
    public sealed class EmptyUserNickNameException : DomainException
    {
        internal EmptyUserNickNameException() : base(message: "User nick name cannot be empty!")
        {
        }
    }
}
