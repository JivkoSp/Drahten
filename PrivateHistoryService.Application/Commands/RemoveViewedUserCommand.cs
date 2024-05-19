
namespace PrivateHistoryService.Application.Commands
{
    public record RemoveViewedUserCommand(Guid ViewerUserId, Guid ViewedUserId, DateTimeOffset DateTime) : ICommand;
}
