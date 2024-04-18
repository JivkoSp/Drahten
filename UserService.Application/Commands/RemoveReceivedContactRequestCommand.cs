
namespace UserService.Application.Commands
{
    public record RemoveReceivedContactRequestCommand(Guid ReceiverUserId, Guid IssuerUserId) : ICommand;
}
