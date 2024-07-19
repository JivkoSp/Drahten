
namespace TopicArticleService.Tests.Integration.Events
{
    public record DislikedArticleCommentAdded(Guid ArticleCommentId) : ITestEvent;
}
