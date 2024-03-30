
namespace TopicArticleService.Domain.Exceptions
{
    internal class EmptyTopicNameException : DomainException
    {
        internal EmptyTopicNameException() : base(message: "Topic name cannot be empty!")
        {
        }
    }
}
