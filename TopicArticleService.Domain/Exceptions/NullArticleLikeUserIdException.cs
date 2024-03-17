
namespace TopicArticleService.Domain.Exceptions
{
    public class NullArticleLikeUserIdException : DomainException
    {
        public NullArticleLikeUserIdException() : base(message: "ArticleLike userId cannot be null!")
        {
        }
    }
}
