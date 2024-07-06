
namespace PublicHistoryService.Application.Commands
{
    public record AddSearchedTopicDataCommand(Guid TopicId, Guid UserId, string SearchedData, DateTimeOffset DateTime) : ICommand;
}
