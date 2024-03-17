
namespace TopicArticleService.Domain.Exceptions
{
    public class ArticleCommentNotFoundException : DomainException
    {
        public ArticleCommentNotFoundException(Guid articleCommentId, string userId) 
            : base(message: $"ArticleComment #{articleCommentId} from user #{userId} was NOT found!")
        {
        }
    }
}
