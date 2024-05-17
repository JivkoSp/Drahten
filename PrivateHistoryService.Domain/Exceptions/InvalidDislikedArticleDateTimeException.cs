
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class InvalidDislikedArticleDateTimeException : DomainException
    {
        internal InvalidDislikedArticleDateTimeException() : base(message: "Invalid datetime for disliked article!")
        {
        }
    }
}
