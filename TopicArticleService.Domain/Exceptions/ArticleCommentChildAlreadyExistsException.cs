
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class ArticleCommentChildAlreadyExistsException : DomainException
    {
        internal ArticleCommentChildAlreadyExistsException(Guid articleCommentId, Guid userId) 
            : base(message: $"ArticleComment #{articleCommentId} already has comment from user #{userId}!")
        {
        }
    }
}
