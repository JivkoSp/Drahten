
namespace TopicArticleService.Domain.Exceptions
{
    internal class InvalidArticleDislikeDateTimeException : DomainException
    {
        internal InvalidArticleDislikeDateTimeException() : base(message: "Invalid ArticleDislike datetime!")
        {
        }
    }
}
