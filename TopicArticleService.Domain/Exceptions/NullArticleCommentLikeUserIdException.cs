
namespace TopicArticleService.Domain.Exceptions
{
    public class NullArticleCommentLikeUserIdException : DomainException
    {
        public NullArticleCommentLikeUserIdException() : base(message: "ArticleCommentLike userId cannot be null!")
        {
        }
    }
}
