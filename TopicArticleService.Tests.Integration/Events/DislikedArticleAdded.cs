
namespace TopicArticleService.Tests.Integration.Events
{
    public record DislikedArticleAdded(string ArticleId) : ITestEvent;
}
