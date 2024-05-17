
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class InvalidViewedArticleDateTimeException : DomainException
    {
        internal InvalidViewedArticleDateTimeException() : base(message: "Invalid datetime for viewed article!")
        {
        }
    }
}
