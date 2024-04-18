
namespace UserService.Application.Commands
{
    public record RemoveIssuedContactRequestCommand(Guid IssuerUserId, Guid ReceiverUserId) : ICommand;
}
