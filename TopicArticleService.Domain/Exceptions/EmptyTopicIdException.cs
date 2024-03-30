
namespace TopicArticleService.Domain.Exceptions
{
    internal class EmptyTopicIdException : DomainException
    {
        internal EmptyTopicIdException() : base(message: "Topic id cannot be empty!")
        {
        }
    }
}
