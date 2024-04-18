
namespace UserService.Domain.Exceptions
{
    public sealed class InvalidBannedUserDateTimeException : DomainException
    {
        public InvalidBannedUserDateTimeException() : base(message: "Invalid datetime for user ban!")
        {
        }
    }
}
