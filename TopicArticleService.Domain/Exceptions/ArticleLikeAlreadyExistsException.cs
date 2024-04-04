
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class ArticleLikeAlreadyExistsException : DomainException
    {
        internal ArticleLikeAlreadyExistsException(Guid articleId, Guid userId) 
            : base(message: $"Article #{articleId} already has like from user #{userId}!")
        {
        }
    }
}
