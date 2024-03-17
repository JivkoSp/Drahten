
namespace TopicArticleService.Domain.Exceptions
{
    public class EmptyArticlePublishingDateException : DomainException
    {
        public EmptyArticlePublishingDateException() : base(message: "Article publishing date cannot be empty!")
        {
        }
    }
}
