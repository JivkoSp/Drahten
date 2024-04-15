
namespace UserService.Application.Commands
{
    public record AddToAuditTrailCommand(Guid UserId, string Action, DateTimeOffset DateTime, string Referrer) : ICommand;
}
