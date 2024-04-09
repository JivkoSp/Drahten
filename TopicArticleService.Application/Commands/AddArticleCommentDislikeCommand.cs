
namespace TopicArticleService.Application.Commands
{
    public record AddArticleCommentDislikeCommand(Guid ArticleCommentId, DateTimeOffset DateTime, Guid UserId) : ICommand;
}
