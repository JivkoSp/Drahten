
namespace TopicArticleService.Tests.Integration.Events
{
    public record LikedArticleCommentAdded(Guid ArticleCommentId) : ITestEvent;
}
