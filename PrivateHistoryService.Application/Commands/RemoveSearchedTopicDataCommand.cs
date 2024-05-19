
namespace PrivateHistoryService.Application.Commands
{
    public record RemoveSearchedTopicDataCommand(Guid TopicId, Guid UserId, string SearchedData, DateTimeOffset DateTime) : ICommand;
}
