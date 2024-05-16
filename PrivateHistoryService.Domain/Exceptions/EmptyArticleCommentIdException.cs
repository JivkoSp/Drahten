
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class EmptyArticleCommentIdException : DomainException
    {
        public EmptyArticleCommentIdException() : base(message: "ArticleComment id cannot be empty!")
        {
        }
    }
}
