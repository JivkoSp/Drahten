
namespace TopicArticleService.Domain.Exceptions
{
    public class EmptyArticleLinkException : DomainException
    {
        public EmptyArticleLinkException() : base(message: "Article link cannot be empty!")
        {
        }
    }
}
