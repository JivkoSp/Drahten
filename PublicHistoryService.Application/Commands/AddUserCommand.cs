
namespace PublicHistoryService.Application.Commands
{
    public record AddUserCommand(Guid UserId) : ICommand;
}
