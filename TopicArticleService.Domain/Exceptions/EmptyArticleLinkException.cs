
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class EmptyArticleLinkException : DomainException
    {
        internal EmptyArticleLinkException() : base(message: "Article link cannot be empty!")
        {
        }
    }
}
