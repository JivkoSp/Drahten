
namespace PrivateHistoryService.Application.Commands
{
    public record AddUserCommand(Guid UserId) : ICommand;
}
