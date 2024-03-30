
namespace TopicArticleService.Domain.Exceptions
{
    internal class EmptyArticleTitleException : DomainException
    {
        internal EmptyArticleTitleException() : base(message: "Article title cannot be empty!")
        {
        }
    }
}
