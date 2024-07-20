
namespace TopicArticleService.Tests.Integration.Events
{
    public record TopicSubscriptionAdded(Guid TopicId) : ITestEvent;
}
