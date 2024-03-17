
namespace TopicArticleService.Domain.Exceptions
{
    public class ArticleCommentAlreadyExistsException : DomainException
    {
        public ArticleCommentAlreadyExistsException(Guid articleId, string userId) 
            : base(message: $"Article #{articleId} already has comment from user #{userId}!")
        {
        }
    }
}
