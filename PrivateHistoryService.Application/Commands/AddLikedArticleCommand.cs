
namespace PrivateHistoryService.Application.Commands
{
    public record AddLikedArticleCommand(Guid ArticleId, Guid UserId, DateTimeOffset DateTime) : ICommand;
}
