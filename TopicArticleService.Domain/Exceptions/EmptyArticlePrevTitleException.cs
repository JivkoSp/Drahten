
namespace TopicArticleService.Domain.Exceptions
{
    public class EmptyArticlePrevTitleException : DomainException
    {
        public EmptyArticlePrevTitleException() : base(message: "Article prev title cannot be empty!")
        {
        }
    }
}
