
namespace TopicArticleService.Domain.Exceptions
{
    public class ArticleCommentLikeAlreadyExistsException : DomainException
    {
        public ArticleCommentLikeAlreadyExistsException(Guid articleCommentId, string userId) 
            : base(message: $"ArticleComment #{articleCommentId} already has like from user #{userId}!")
        {
        }
    }
}
