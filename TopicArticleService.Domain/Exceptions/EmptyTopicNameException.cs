
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class EmptyTopicNameException : DomainException
    {
        internal EmptyTopicNameException() : base(message: "Topic name cannot be empty!")
        {
        }
    }
}
