
using TopicArticleService.Tests.Integration.Events;

namespace TopicArticleService.Tests.Integration.EventProcessing
{
    public interface IEventProcessor
    {
        static List<ITestEvent> Events = new List<ITestEvent>();
        void ProcessEvent(string message);
    }
}
