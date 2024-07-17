
namespace TopicArticleService.Tests.Integration.Events
{
    public record ViewedArticleAdded(string ArticleId) : ITestEvent;
}
