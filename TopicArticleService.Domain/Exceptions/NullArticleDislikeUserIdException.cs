
namespace TopicArticleService.Domain.Exceptions
{
    public class NullArticleDislikeUserIdException : DomainException
    {
        public NullArticleDislikeUserIdException() : base(message: "ArticleDislike userId cannot be null!")
        {
        }
    }
}
