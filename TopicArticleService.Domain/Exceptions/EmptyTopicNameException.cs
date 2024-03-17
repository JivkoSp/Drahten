
namespace TopicArticleService.Domain.Exceptions
{
    public class EmptyTopicNameException : DomainException
    {
        public EmptyTopicNameException() : base(message: "Topic name cannot be empty!")
        {
        }
    }
}
