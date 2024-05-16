
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class EmptyArticleIdException : DomainException
    {
        public EmptyArticleIdException() : base(message: "Article id cannot be empty!")
        {
        }
    }
}
