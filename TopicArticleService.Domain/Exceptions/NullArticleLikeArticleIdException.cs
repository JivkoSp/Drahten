
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class NullArticleLikeArticleIdException : DomainException
    {
        internal NullArticleLikeArticleIdException() : base(message: "ArticleLike articleId cannot be null!")
        {
        }
    }
}
