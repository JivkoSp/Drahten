
namespace TopicArticleService.Domain.Exceptions
{
    public class EmptyArticleAuthorException : DomainException
    {
        public EmptyArticleAuthorException() : base(message: "Article author cannot be empty!")
        {
        }
    }
}
