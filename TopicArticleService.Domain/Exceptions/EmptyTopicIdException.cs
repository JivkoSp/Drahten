
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class EmptyTopicIdException : DomainException
    {
        internal EmptyTopicIdException() : base(message: "Topic id cannot be empty!")
        {
        }
    }
}
