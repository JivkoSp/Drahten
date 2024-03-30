
namespace TopicArticleService.Domain.Exceptions
{
    internal class NullArticleLikeUserIdException : DomainException
    {
        internal NullArticleLikeUserIdException() : base(message: "ArticleLike userId cannot be null!")
        {
        }
    }
}
