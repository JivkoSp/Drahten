
namespace PublicHistoryService.Application.Commands
{
    public record AddViewedUserCommand(Guid ViewerUserId, Guid ViewedUserId, DateTimeOffset DateTime) : ICommand;
}
