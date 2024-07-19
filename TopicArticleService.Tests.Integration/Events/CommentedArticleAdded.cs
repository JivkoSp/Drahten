
namespace TopicArticleService.Tests.Integration.Events
{
    public record CommentedArticleAdded(string ArticleId) : ITestEvent;
}
