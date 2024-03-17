
namespace TopicArticleService.Domain.Exceptions
{
    public class InvalidArticleLikeDateTimeException : DomainException
    {
        public InvalidArticleLikeDateTimeException() : base(message: "Invalid ArticleLike datetime!")
        {
        }
    }
}
