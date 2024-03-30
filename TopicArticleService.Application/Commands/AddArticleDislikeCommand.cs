
namespace TopicArticleService.Application.Commands
{
    public record AddArticleDislikeCommand(Guid ArticleId, string DateTime, Guid UserId) : ICommand;
}
