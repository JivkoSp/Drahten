
namespace TopicArticleService.Domain.Exceptions
{
    internal class InvalidArticleCommentDislikeDateTimeException : DomainException
    {
        internal InvalidArticleCommentDislikeDateTimeException() : base(message: "Invalid ArticleCommentDislike datetime!")
        {
        }
    }
}
