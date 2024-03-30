
namespace TopicArticleService.Application.Commands
{
    public record RegisterUserCommand(Guid UserId) : ICommand;
}
