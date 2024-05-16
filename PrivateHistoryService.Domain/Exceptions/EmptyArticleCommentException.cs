
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class EmptyArticleCommentException : DomainException
    {
        public EmptyArticleCommentException() : base(message: "Article comment cannot be empty!")
        {
        }
    }
}
