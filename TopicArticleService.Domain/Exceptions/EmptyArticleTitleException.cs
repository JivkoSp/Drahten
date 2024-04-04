
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class EmptyArticleTitleException : DomainException
    {
        internal EmptyArticleTitleException() : base(message: "Article title cannot be empty!")
        {
        }
    }
}
