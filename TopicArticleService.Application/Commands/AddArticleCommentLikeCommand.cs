
namespace TopicArticleService.Application.Commands
{
    public record AddArticleCommentLikeCommand(Guid ArticleId, Guid ArticleCommentId, string DateTime, Guid UserId) : ICommand;
}