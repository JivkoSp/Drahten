
namespace UserService.Domain.Exceptions
{
    public sealed class InvalidContactRequestDateTimeException : DomainException
    {
        internal InvalidContactRequestDateTimeException() : base(message: "Invalid datetime for contact request!")
        {
        }
    }
}
