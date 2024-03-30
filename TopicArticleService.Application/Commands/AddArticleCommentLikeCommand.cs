
namespace TopicArticleService.Application.Commands
{
    public record AddArticleCommentLikeCommand(Guid ArticleId, Guid ArticleCommentId) : ICommand;
}


//IArticleService.FindArticleCommentByIdAsync(ArticleCommentId) -> ArticleComment
//ArticleComment.AddLike() / AddDisLike()