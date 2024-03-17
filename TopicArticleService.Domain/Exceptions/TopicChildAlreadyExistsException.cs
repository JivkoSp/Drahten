
namespace TopicArticleService.Domain.Exceptions
{
    public class TopicChildAlreadyExistsException : DomainException
    {
        public TopicChildAlreadyExistsException(Guid topicId, string topicName) 
            : base(message: $"Topic #{topicId} already has topic child with name {topicName}!")
        {
        }
    }
}
