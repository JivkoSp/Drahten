
namespace TopicArticleService.Domain.Exceptions
{
    internal class InvalidArticleLikeDateTimeException : DomainException
    {
        internal InvalidArticleLikeDateTimeException() : base(message: "Invalid ArticleLike datetime!")
        {
        }
    }
}
