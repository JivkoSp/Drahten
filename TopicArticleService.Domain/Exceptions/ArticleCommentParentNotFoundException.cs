
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class ArticleCommentParentNotFoundException : DomainException
    {
        internal ArticleCommentParentNotFoundException(Guid articleParentCommentId, Guid articleCommentId) 
            : base(message: $"Parent comment #{articleParentCommentId} for article comment #{articleCommentId} was NOT found!")
        {
        }
    }
}
