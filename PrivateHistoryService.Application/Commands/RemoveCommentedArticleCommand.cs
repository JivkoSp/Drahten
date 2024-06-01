
namespace PrivateHistoryService.Application.Commands
{
    public record RemoveCommentedArticleCommand(Guid UserId, Guid CommentedArticleId) : ICommand;
}
