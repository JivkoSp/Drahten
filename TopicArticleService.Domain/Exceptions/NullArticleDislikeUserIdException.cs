
namespace TopicArticleService.Domain.Exceptions
{
    internal class NullArticleDislikeUserIdException : DomainException
    {
        internal NullArticleDislikeUserIdException() : base(message: "ArticleDislike userId cannot be null!")
        {
        }
    }
}
