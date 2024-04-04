
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class EmptyArticleIdException : DomainException
    {
        internal EmptyArticleIdException() : base(message: "Article id cannot be empty!")
        {
        }
    }
}
