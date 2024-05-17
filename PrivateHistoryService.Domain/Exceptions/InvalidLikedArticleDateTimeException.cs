
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class InvalidLikedArticleDateTimeException : DomainException
    {
        internal InvalidLikedArticleDateTimeException() : base(message: "Invalid datetime for liked article!")
        {
        }
    }
}
