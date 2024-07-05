
namespace PublicHistoryService.Domain.Exceptions
{
    public sealed class NullArticleCommentException : DomainException
    {
        internal NullArticleCommentException() : base(message: "Article comment cannot be null!")
        {
        }
    }
}
