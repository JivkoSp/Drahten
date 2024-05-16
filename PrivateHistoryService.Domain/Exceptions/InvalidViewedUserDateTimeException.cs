
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class InvalidViewedUserDateTimeException : DomainException
    {
        public InvalidViewedUserDateTimeException() : base(message: "Invalid datetime for viewed user!")
        {
        }
    }
}
