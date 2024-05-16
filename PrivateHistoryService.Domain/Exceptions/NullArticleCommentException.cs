
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class NullArticleCommentException : DomainException
    {
        public NullArticleCommentException() : base(message: "Article comment cannot be null!")
        {
        }
    }
}
