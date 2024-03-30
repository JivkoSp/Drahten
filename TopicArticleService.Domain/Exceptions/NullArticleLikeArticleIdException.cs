
namespace TopicArticleService.Domain.Exceptions
{
    internal class NullArticleLikeArticleIdException : DomainException
    {
        public NullArticleLikeArticleIdException() : base(message: "ArticleLike articleId cannot be null!")
        {
        }
    }
}
