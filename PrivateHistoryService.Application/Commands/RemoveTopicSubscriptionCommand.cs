
namespace PrivateHistoryService.Application.Commands
{
    public record RemoveTopicSubscriptionCommand(Guid TopicId, Guid UserId) : ICommand;
}
