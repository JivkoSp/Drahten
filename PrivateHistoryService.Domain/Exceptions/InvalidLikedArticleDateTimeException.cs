
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class InvalidLikedArticleDateTimeException : DomainException
    {
        public InvalidLikedArticleDateTimeException() : base(message: "Invalid datetime for liked article!")
        {
        }
    }
}
