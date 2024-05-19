
namespace PrivateHistoryService.Application.Commands
{
    public record AddViewedUserCommand(Guid ViewerUserId, Guid ViewedUserId, DateTimeOffset DateTime) : ICommand;
}
