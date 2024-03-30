
namespace TopicArticleService.Domain.Exceptions
{
    internal class ArticleCommentLikeAlreadyExistsException : DomainException
    {
        internal ArticleCommentLikeAlreadyExistsException(Guid articleCommentId, Guid userId) 
            : base(message: $"ArticleComment #{articleCommentId} already has like from user #{userId}!")
        {
        }
    }
}
