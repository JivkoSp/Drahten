
namespace TopicArticleService.Domain.Exceptions
{
    internal class EmptyArticleCommentIdException : DomainException
    {
        internal EmptyArticleCommentIdException() : base(message: "ArticleComment id cannot be empty!")
        {
        }
    }
}
