
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class EmptyArticleCommentValueException : DomainException
    {
        internal EmptyArticleCommentValueException() : base(message: "Article comment value cannot be empty!")
        {
        }
    }
}
