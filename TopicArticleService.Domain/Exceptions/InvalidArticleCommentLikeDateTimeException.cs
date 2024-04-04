
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class InvalidArticleCommentLikeDateTimeException : DomainException
    {
        internal InvalidArticleCommentLikeDateTimeException() : base(message: "Invalid ArticleCommentLike datetime!")
        {
        }
    }
}
