
namespace PublicHistoryService.Application.Commands
{
    public record RemoveSearchedTopicDataCommand(Guid UserId, Guid SearchedTopicDataId) : ICommand;
}
