
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class NullArticleIdException : DomainException
    {
        public NullArticleIdException() : base(message: "Article id cannot be null!")
        {
        }
    }
}
