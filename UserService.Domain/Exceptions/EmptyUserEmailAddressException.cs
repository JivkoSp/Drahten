
namespace UserService.Domain.Exceptions
{
    public sealed class EmptyUserEmailAddressException : DomainException
    {
        internal EmptyUserEmailAddressException() : base(message: "User email address cannot be empty!")
        {
        }
    }
}
