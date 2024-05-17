
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class InvalidSearchedArticleDataDateTimeException : DomainException
    {
        internal InvalidSearchedArticleDataDateTimeException() : base(message: "Invalid datetime for searched article data!")
        {
        }
    }
}
