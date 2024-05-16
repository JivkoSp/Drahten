
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class InvalidCommentedArticleDateTimeException : DomainException
    {
        public InvalidCommentedArticleDateTimeException() : base(message: "Invalid datetime for commented article!")
        {
        }
    }
}
