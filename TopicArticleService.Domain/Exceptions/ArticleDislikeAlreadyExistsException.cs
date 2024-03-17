
namespace TopicArticleService.Domain.Exceptions
{
    public class ArticleDislikeAlreadyExistsException : DomainException
    {
        public ArticleDislikeAlreadyExistsException(Guid articleId, string userId) 
            : base(message: $"Article #{articleId} already has dislike from user #{userId}!")
        {
        }
    }
}
