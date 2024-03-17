
namespace TopicArticleService.Domain.Exceptions
{
    public class ArticleCommentDisLikeAlreadyExistsException : DomainException
    {
        public ArticleCommentDisLikeAlreadyExistsException(Guid articleCommentId, string userId) 
            : base(message: $"ArticleComment #{articleCommentId} already has dislike from user #{userId}!")
        {
        }
    }
}
