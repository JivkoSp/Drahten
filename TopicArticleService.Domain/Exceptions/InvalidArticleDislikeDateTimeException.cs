
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class InvalidArticleDislikeDateTimeException : DomainException
    {
        internal InvalidArticleDislikeDateTimeException() : base(message: "Invalid ArticleDislike datetime!")
        {
        }
    }
}
