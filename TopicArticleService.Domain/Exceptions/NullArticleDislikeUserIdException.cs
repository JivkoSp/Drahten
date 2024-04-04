
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class NullArticleDislikeUserIdException : DomainException
    {
        internal NullArticleDislikeUserIdException() : base(message: "ArticleDislike userId cannot be null!")
        {
        }
    }
}
