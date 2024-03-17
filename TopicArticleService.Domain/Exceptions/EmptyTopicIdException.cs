
namespace TopicArticleService.Domain.Exceptions
{
    public class EmptyTopicIdException : DomainException
    {
        public EmptyTopicIdException() : base(message: "Topic id cannot be empty!")
        {
        }
    }
}
