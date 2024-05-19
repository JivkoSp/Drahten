
namespace PrivateHistoryService.Application.Commands
{
    public record AddTopicSubscriptionCommand(Guid TopicId, Guid UserId, DateTimeOffset DateTime) : ICommand;
}
