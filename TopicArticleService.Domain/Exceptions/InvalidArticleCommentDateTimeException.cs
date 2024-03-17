
namespace TopicArticleService.Domain.Exceptions
{
    public class InvalidArticleCommentDateTimeException : DomainException
    {
        public InvalidArticleCommentDateTimeException() : base(message: "Invalid ArticleComment datetime!")
        {
        }
    }
}
