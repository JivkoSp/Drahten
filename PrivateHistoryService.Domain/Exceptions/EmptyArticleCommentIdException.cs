
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class EmptyArticleCommentIdException : DomainException
    {
        internal EmptyArticleCommentIdException() : base(message: "ArticleComment id cannot be empty!")
        {
        }
    }
}
