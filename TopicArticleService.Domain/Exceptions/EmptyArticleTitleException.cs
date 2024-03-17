
namespace TopicArticleService.Domain.Exceptions
{
    public class EmptyArticleTitleException : DomainException
    {
        public EmptyArticleTitleException() : base(message: "Article title cannot be empty!")
        {
        }
    }
}
