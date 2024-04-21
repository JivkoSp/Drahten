
namespace TopicArticleService.Application.Commands
{
    public record RegisterUserTopicCommand(Guid UserId, Guid TopicId, DateTimeOffset DateTime) : ICommand;
}
