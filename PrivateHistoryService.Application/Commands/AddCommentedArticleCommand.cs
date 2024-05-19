
namespace PrivateHistoryService.Application.Commands
{
    public record AddCommentedArticleCommand(Guid ArticleId, Guid UserId, string ArticleComment, DateTimeOffset DateTime) : ICommand;
}
