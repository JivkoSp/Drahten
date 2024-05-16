
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class InvalidSearchedArticleDataDateTimeException : DomainException
    {
        public InvalidSearchedArticleDataDateTimeException() : base(message: "Invalid datetime for searched article data!")
        {
        }
    }
}
