
namespace PublicHistoryService.Application.Commands
{
    public record RemoveViewedUserCommand(Guid ViewerUserId, Guid ViewedUserId) : ICommand;
}
