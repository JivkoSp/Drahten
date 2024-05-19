
namespace PrivateHistoryService.Application.Commands
{
    public record AddViewedArticleCommand(Guid ArticleId, Guid UserId, DateTimeOffset DateTime) : ICommand;
}
