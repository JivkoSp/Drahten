
namespace PrivateHistoryService.Application.Commands
{
    public record AddDislikedArticleCommand(Guid ArticleId, Guid UserId, DateTimeOffset DateTime) : ICommand;
}
