
namespace TopicArticleService.Domain.Exceptions
{
    internal class NullArticleDislikeArticleIdException : DomainException
    {
        public NullArticleDislikeArticleIdException() : base(message: "ArticleDislike articleId cannot be null!")
        {
        }
    }
}
