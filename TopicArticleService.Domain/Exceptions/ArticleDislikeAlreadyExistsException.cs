
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class ArticleDislikeAlreadyExistsException : DomainException
    {
        internal ArticleDislikeAlreadyExistsException(Guid articleId, Guid userId) 
            : base(message: $"Article #{articleId} already has dislike from user #{userId}!")
        {
        }
    }
}
