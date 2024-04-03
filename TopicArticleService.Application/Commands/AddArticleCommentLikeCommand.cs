
namespace TopicArticleService.Application.Commands
{
    public record AddArticleCommentLikeCommand(Guid ArticleCommentId, string DateTime, Guid UserId) : ICommand;
}