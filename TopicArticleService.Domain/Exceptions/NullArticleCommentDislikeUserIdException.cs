
namespace TopicArticleService.Domain.Exceptions
{
    public class NullArticleCommentDislikeUserIdException : DomainException
    {
        public NullArticleCommentDislikeUserIdException() : base(message: "ArticleCommentDislike userId cannot be null!")
        {
        }
    }
}
