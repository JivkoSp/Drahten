
namespace PrivateHistoryService.Application.Commands
{
    public record RemoveCommentedArticleCommand(Guid ArticleId, Guid UserId, string ArticleComment, DateTimeOffset DateTime) : ICommand;
}
