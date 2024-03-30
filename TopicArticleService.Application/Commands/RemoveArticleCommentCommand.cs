
namespace TopicArticleService.Application.Commands
{
    public record RemoveArticleCommentCommand(Guid ArticleId, Guid ArticleCommentId, Guid? ParentArticleCommentId) : ICommand;
}
