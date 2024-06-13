
namespace PrivateHistoryService.Application.Commands
{
    public record SetUserRetentionDateTimeCommand(Guid UserId, DateTimeOffset DateTime) : ICommand;
}
