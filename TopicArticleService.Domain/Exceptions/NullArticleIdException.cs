
namespace TopicArticleService.Domain.Exceptions
{
    internal class NullArticleIdException : DomainException
    {
        public NullArticleIdException() : base(message: "ArticleId cannot be null!")
        {
        }
    }
}
