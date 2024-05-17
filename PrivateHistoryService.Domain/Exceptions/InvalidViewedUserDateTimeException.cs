
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class InvalidViewedUserDateTimeException : DomainException
    {
        internal InvalidViewedUserDateTimeException() : base(message: "Invalid datetime for viewed user!")
        {
        }
    }
}
