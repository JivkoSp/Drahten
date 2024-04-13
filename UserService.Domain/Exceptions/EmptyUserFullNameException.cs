
namespace UserService.Domain.Exceptions
{
    public sealed class EmptyUserFullNameException : DomainException
    {
        internal EmptyUserFullNameException() : base(message: "User full name cannot be empty!")
        {
        }
    }
}
