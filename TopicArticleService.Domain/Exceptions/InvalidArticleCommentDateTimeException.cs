
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class InvalidArticleCommentDateTimeException : DomainException
    {
        internal InvalidArticleCommentDateTimeException() : base(message: "Invalid ArticleComment datetime!")
        {
        }
    }
}
