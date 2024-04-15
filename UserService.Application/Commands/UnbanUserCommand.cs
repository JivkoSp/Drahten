
namespace UserService.Application.Commands
{
    public record UnbanUserCommand(Guid IssuerUserId, Guid ReceiverUserId) : ICommand;
}
