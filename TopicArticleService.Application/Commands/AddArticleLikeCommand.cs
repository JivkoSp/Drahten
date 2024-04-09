
namespace TopicArticleService.Application.Commands
{
    public record AddArticleLikeCommand(Guid ArticleId, DateTimeOffset DateTime, Guid UserId) : ICommand;
}
