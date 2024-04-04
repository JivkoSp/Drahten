
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class TopicChildAlreadyExistsException : DomainException
    {
        internal TopicChildAlreadyExistsException(Guid topicId, string topicName) 
            : base(message: $"Topic #{topicId} already has topic child with name {topicName}!")
        {
        }
    }
}
