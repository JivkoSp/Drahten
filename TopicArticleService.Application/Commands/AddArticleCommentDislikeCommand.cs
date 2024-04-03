
namespace TopicArticleService.Application.Commands
{
    public record AddArticleCommentDislikeCommand(Guid ArticleCommentId, string DateTime, Guid UserId) : ICommand;
}
