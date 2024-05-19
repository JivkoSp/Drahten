
namespace PrivateHistoryService.Application.Commands
{
    public record RemovedSearchedArticleDataCommand(Guid ArticleId, Guid UserId, string SearchedData, DateTimeOffset DateTime) : ICommand;
}
