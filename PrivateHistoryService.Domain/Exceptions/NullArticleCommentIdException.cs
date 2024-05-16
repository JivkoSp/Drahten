
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class NullArticleCommentIdException : DomainException
    {
        public NullArticleCommentIdException() : base(message: "ArticleComment id cannot be empty!")
        {
        }
    }
}
