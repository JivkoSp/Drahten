
namespace TopicArticleService.Application.Commands
{
    public record AddArticleCommentCommand(Guid ArticleId, Guid ArticleCommentId, string CommentValue, DateTimeOffset DateTime, 
            Guid UserId, Guid? ParentArticleCommentId) : ICommand;
}
