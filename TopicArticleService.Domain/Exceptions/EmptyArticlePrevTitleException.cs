
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class EmptyArticlePrevTitleException : DomainException
    {
        internal EmptyArticlePrevTitleException() : base(message: "Article prev title cannot be empty!")
        {
        }
    }
}
