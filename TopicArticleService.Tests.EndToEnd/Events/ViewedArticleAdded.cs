
namespace TopicArticleService.Tests.EndToEnd.Events
{
    public record ViewedArticleAdded(string ArticleId) : ITestEvent;
}
