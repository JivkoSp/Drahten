
namespace TopicArticleService.Domain.Exceptions
{
    internal class ArticleCommentDisLikeAlreadyExistsException : DomainException
    {
        internal ArticleCommentDisLikeAlreadyExistsException(Guid articleCommentId, Guid userId) 
            : base(message: $"ArticleComment #{articleCommentId} already has dislike from user #{userId}!")
        {
        }
    }
}
