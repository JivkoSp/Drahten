
namespace TopicArticleService.Domain.Exceptions
{
    internal class EmptyArticleCommentValueException : DomainException
    {
        internal EmptyArticleCommentValueException() : base(message: "Article comment value cannot be empty!")
        {
        }
    }
}
