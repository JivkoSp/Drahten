
namespace PrivateHistoryService.Application.Commands
{
    public record AddLikedArticleCommentCommand(Guid ArticleId, Guid UserId, Guid ArticleCommentId, DateTimeOffset DateTime) : ICommand;
}
