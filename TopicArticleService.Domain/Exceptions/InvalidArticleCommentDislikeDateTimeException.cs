
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class InvalidArticleCommentDislikeDateTimeException : DomainException
    {
        internal InvalidArticleCommentDislikeDateTimeException() : base(message: "Invalid ArticleCommentDislike datetime!")
        {
        }
    }
}
