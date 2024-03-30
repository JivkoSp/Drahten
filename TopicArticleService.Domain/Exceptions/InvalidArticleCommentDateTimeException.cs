
namespace TopicArticleService.Domain.Exceptions
{
    internal class InvalidArticleCommentDateTimeException : DomainException
    {
        internal InvalidArticleCommentDateTimeException() : base(message: "Invalid ArticleComment datetime!")
        {
        }
    }
}
