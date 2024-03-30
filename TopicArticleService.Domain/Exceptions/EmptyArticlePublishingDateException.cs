
namespace TopicArticleService.Domain.Exceptions
{
    internal class EmptyArticlePublishingDateException : DomainException
    {
        internal EmptyArticlePublishingDateException() : base(message: "Article publishing date cannot be empty!")
        {
        }
    }
}
