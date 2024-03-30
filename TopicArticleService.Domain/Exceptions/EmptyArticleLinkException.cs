
namespace TopicArticleService.Domain.Exceptions
{
    internal class EmptyArticleLinkException : DomainException
    {
        internal EmptyArticleLinkException() : base(message: "Article link cannot be empty!")
        {
        }
    }
}
