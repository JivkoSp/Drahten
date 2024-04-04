
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class NullArticleDislikeArticleIdException : DomainException
    {
        internal NullArticleDislikeArticleIdException() : base(message: "ArticleDislike articleId cannot be null!")
        {
        }
    }
}
