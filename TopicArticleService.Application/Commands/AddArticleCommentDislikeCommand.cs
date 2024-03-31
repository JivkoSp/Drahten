
namespace TopicArticleService.Application.Commands
{
    public record AddArticleCommentDislikeCommand(Guid ArticleId, Guid ArticleCommentId, string DateTime, Guid UserId) : ICommand;
}
