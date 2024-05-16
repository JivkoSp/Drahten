
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class InvalidViewedArticleDateTimeException : DomainException
    {
        public InvalidViewedArticleDateTimeException() : base(message: "Invalid datetime for viewed article!")
        {
        }
    }
}
