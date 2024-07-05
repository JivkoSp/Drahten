
namespace PublicHistoryService.Domain.Exceptions
{
    public sealed class InvalidCommentedArticleDateTimeException : DomainException
    {
        internal InvalidCommentedArticleDateTimeException() : base(message: "Invalid datetime for commented article!")
        {
        }
    }
}
