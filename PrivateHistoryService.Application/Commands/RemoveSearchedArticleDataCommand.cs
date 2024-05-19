
namespace PrivateHistoryService.Application.Commands
{
    public record RemoveSearchedArticleDataCommand(Guid ArticleId, Guid UserId, string SearchedData, DateTimeOffset DateTime) : ICommand;
}
