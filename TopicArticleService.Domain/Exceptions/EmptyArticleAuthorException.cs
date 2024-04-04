
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class EmptyArticleAuthorException : DomainException
    {
        internal EmptyArticleAuthorException() : base(message: "Article author cannot be empty!")
        {
        }
    }
}
