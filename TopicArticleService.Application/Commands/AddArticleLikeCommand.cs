
namespace TopicArticleService.Application.Commands
{
    public record AddArticleLikeCommand(Guid ArticleId, string DateTime, Guid UserId) : ICommand;
}
