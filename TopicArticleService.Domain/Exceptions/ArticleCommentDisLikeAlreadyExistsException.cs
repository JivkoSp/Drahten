
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class ArticleCommentDisLikeAlreadyExistsException : DomainException
    {
        internal ArticleCommentDisLikeAlreadyExistsException(Guid articleCommentId, Guid userId) 
            : base(message: $"ArticleComment #{articleCommentId} already has dislike from user #{userId}!")
        {
        }
    }
}
