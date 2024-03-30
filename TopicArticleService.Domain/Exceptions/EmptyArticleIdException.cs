
namespace TopicArticleService.Domain.Exceptions
{
    internal class EmptyArticleIdException : DomainException
    {
        internal EmptyArticleIdException() : base(message: "Article id cannot be empty!")
        {
        }
    }
}
