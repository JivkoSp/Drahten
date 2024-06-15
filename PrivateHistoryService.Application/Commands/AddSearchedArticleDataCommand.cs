
namespace PrivateHistoryService.Application.Commands
{
    public record AddSearchedArticleDataCommand(Guid ArticleId, Guid UserId, string SearchedData, 
        string SearchedDataAnswer, DateTimeOffset DateTime) : ICommand;
}
