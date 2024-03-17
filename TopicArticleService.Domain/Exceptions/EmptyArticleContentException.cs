
namespace TopicArticleService.Domain.Exceptions
{
    public class EmptyArticleContentException : DomainException
    {
        public EmptyArticleContentException() : base(message: "Article content cannot be empty!")
        {
        }
    }
}
