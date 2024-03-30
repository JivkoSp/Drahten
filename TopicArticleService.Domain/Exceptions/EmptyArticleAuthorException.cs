
namespace TopicArticleService.Domain.Exceptions
{
    internal class EmptyArticleAuthorException : DomainException
    {
        internal EmptyArticleAuthorException() : base(message: "Article author cannot be empty!")
        {
        }
    }
}
