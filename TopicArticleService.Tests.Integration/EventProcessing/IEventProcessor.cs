
namespace TopicArticleService.Tests.Integration.EventProcessing
{
    internal interface IEventProcessor
    {
        Task ProcessEventAsync(string message);
    }
}
