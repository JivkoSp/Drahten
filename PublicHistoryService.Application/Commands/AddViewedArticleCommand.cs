
namespace PublicHistoryService.Application.Commands
{
    public record AddViewedArticleCommand(Guid ArticleId, Guid UserId, DateTimeOffset DateTime) : ICommand;
}
