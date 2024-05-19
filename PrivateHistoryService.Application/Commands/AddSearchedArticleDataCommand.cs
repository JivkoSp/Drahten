
namespace PrivateHistoryService.Application.Commands
{
    public record AddSearchedArticleDataCommand(Guid ArticleId, Guid UserId, string SearchedData, DateTimeOffset DateTime) : ICommand;
}
