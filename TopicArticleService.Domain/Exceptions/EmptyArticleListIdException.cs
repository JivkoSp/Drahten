
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class EmptyArticleListIdException : DomainException
    {
        internal EmptyArticleListIdException() : base(message: "ArticleList id cannot be empty!")
        {
        }
    }
}
