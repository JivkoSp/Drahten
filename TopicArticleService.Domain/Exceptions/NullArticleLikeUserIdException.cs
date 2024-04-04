
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class NullArticleLikeUserIdException : DomainException
    {
        internal NullArticleLikeUserIdException() : base(message: "ArticleLike userId cannot be null!")
        {
        }
    }
}
