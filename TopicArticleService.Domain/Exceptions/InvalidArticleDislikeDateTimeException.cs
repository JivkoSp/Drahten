
namespace TopicArticleService.Domain.Exceptions
{
    public class InvalidArticleDislikeDateTimeException : DomainException
    {
        public InvalidArticleDislikeDateTimeException() : base(message: "Invalid ArticleDislike datetime!")
        {
        }
    }
}
