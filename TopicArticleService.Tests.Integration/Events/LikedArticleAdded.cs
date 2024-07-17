
namespace TopicArticleService.Tests.Integration.Events
{
    public record LikedArticleAdded(string ArticleId) : ITestEvent;
}
