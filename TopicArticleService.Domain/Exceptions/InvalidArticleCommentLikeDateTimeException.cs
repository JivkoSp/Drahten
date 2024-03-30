
namespace TopicArticleService.Domain.Exceptions
{
    internal class InvalidArticleCommentLikeDateTimeException : DomainException
    {
        internal InvalidArticleCommentLikeDateTimeException() : base(message: "Invalid ArticleCommentLike datetime!")
        {
        }
    }
}
