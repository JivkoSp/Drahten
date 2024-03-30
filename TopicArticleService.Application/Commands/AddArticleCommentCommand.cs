
namespace TopicArticleService.Application.Commands
{
    public record AddArticleCommentCommand(Guid ArticleId, Guid ArticleCommentId, string CommentValue, DateTime DateTime, 
            Guid UserId, Guid? ParentArticleCommentId) : ICommand;
}
