
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class EmptyArticleCommentException : DomainException
    {
        internal EmptyArticleCommentException() : base(message: "Article comment cannot be empty!")
        {
        }
    }
}
