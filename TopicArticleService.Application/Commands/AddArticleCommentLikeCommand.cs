
namespace TopicArticleService.Application.Commands
{
    public record AddArticleCommentLikeCommand(Guid ArticleCommentId, DateTimeOffset DateTime, Guid UserId) : ICommand;
}