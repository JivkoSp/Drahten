
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class ArticleCommentNotFoundException : DomainException
    {
        internal ArticleCommentNotFoundException(Guid articleCommentId) 
            : base(message: $"ArticleComment #{articleCommentId} was NOT found!")
        {
        }
    }
}
