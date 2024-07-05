
namespace PublicHistoryService.Domain.Exceptions
{
    public sealed class NullArticleIdException : DomainException
    {
        internal NullArticleIdException() : base(message: "Article id cannot be null!")
        {
        }
    }
}
