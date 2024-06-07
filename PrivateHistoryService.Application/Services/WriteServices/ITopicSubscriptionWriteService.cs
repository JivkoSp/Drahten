using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Application.Services.WriteServices
{
    public interface ITopicSubscriptionWriteService
    {
        Task AddTopicSubscriptionAsync(TopicSubscription topicSubscription);
    }
}
