
namespace UserService.Application.Commands
{
    public record UpdateContactRequestMessageCommand(Guid IssuerUserId, Guid ReceiverUserId, 
        string Message, DateTimeOffset DateTime) : ICommand;
}
