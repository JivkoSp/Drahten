
namespace TopicArticleService.Domain.Exceptions
{
    public class EmptyArticleIdException : DomainException
    {
        public EmptyArticleIdException() : base(message: "Article id cannot be empty!")
        {
        }
    }
}
