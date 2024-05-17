
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class NullArticleCommentIdException : DomainException
    {
        internal NullArticleCommentIdException() : base(message: "ArticleComment id cannot be empty!")
        {
        }
    }
}
