
namespace TopicArticleService.Domain.Exceptions
{
    public class EmptyArticleCommentValueException : DomainException
    {
        public EmptyArticleCommentValueException() : base(message: "Article comment value cannot be empty!")
        {
        }
    }
}
