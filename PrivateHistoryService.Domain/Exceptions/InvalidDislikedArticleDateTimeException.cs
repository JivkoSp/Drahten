
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class InvalidDislikedArticleDateTimeException : DomainException
    {
        public InvalidDislikedArticleDateTimeException() : base(message: "Invalid datetime for disliked article!")
        {
        }
    }
}
