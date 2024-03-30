
namespace TopicArticleService.Domain.Exceptions
{
    internal class ArticleCommentChildAlreadyExistsException : DomainException
    {
        internal ArticleCommentChildAlreadyExistsException(Guid articleCommentId, Guid userId) 
            : base(message: $"ArticleComment #{articleCommentId} already has comment from user #{userId}!")
        {
        }
    }
}
