
namespace TopicArticleService.Application.Commands
{
    public record RegisterUserArticleCommand(Guid ArticleId, Guid UserId) : ICommand;
}
