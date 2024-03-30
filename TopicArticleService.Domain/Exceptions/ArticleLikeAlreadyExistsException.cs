
namespace TopicArticleService.Domain.Exceptions
{
    internal class ArticleLikeAlreadyExistsException : DomainException
    {
        internal ArticleLikeAlreadyExistsException(Guid articleId, Guid userId) 
            : base(message: $"Article #{articleId} already has like from user #{userId}!")
        {
        }
    }
}
