
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class InvalidArticleLikeDateTimeException : DomainException
    {
        internal InvalidArticleLikeDateTimeException() : base(message: "Invalid ArticleLike datetime!")
        {
        }
    }
}
