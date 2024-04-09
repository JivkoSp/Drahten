
namespace TopicArticleService.Application.Commands
{
    public record AddArticleDislikeCommand(Guid ArticleId, DateTimeOffset DateTime, Guid UserId) : ICommand;
}
