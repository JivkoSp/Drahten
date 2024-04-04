
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class EmptyArticlePublishingDateException : DomainException
    {
        internal EmptyArticlePublishingDateException() : base(message: "Article publishing date cannot be empty!")
        {
        }
    }
}
