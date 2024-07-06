
namespace PublicHistoryService.Application.Commands
{
    public record RemoveCommentedArticleCommand(Guid UserId, Guid CommentedArticleId) : ICommand;
}
