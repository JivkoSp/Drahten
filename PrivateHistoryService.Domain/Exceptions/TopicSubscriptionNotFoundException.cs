
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class TopicSubscriptionNotFoundException : DomainException
    {
        internal TopicSubscriptionNotFoundException(Guid topicId, Guid userId) 
            : base(message: $"There is no subscription for topic #{topicId} from user #{userId}!")
        {
        }
    }
}
