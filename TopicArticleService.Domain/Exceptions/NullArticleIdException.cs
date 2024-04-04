
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class NullArticleIdException : DomainException
    {
        internal NullArticleIdException() : base(message: "ArticleId cannot be null!")
        {
        }
    }
}
