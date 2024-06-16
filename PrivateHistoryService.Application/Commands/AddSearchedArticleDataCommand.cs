
namespace PrivateHistoryService.Application.Commands
{
    public record AddSearchedArticleDataCommand(Guid ArticleId, Guid UserId, string SearchedData, 
        string SearchedDataAnswer, string SearchedDataAnswerContext, DateTimeOffset DateTime) : ICommand;
}
