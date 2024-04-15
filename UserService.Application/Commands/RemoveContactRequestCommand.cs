
namespace UserService.Application.Commands
{
    public record RemoveContactRequestCommand(Guid IssuerUserId, Guid ReceiverUserId) : ICommand;
}
