
namespace TopicArticleService.Domain.Exceptions
{
    public class ArticleLikeAlreadyExistsException : DomainException
    {
        public ArticleLikeAlreadyExistsException(Guid articleId, string userId) 
            : base(message: $"Article #{articleId} already has like from user #{userId}!")
        {
        }
    }
}
