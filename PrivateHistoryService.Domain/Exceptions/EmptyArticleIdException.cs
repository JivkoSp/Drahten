
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class EmptyArticleIdException : DomainException
    {
        internal EmptyArticleIdException() : base(message: "Article id cannot be empty!")
        {
        }
    }
}
