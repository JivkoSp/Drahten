
namespace TopicArticleService.Domain.Exceptions
{
    internal class EmptyArticlePrevTitleException : DomainException
    {
        internal EmptyArticlePrevTitleException() : base(message: "Article prev title cannot be empty!")
        {
        }
    }
}
