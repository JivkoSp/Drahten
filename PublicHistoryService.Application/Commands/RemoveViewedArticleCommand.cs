
namespace PublicHistoryService.Application.Commands
{
    public record RemoveViewedArticleCommand(Guid UserId, Guid ViewedArticleId) : ICommand;
}
