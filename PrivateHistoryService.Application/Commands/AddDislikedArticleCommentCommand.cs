
namespace PrivateHistoryService.Application.Commands
{
    public record AddDislikedArticleCommentCommand(Guid ArticleId, Guid UserId, Guid ArticleCommentId, DateTimeOffset DateTime) : ICommand;
}
