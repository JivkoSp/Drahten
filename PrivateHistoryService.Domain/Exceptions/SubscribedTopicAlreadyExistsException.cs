
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class SubscribedTopicAlreadyExistsException : DomainException
    {
        internal SubscribedTopicAlreadyExistsException(Guid topicId, Guid userId) 
            : base(message: $"There is already topic subscription for topic #{topicId} by user #{userId}!")
        {
        }
    }
}
