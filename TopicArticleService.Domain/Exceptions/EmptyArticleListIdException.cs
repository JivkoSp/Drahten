
namespace TopicArticleService.Domain.Exceptions
{
    internal class EmptyArticleListIdException : DomainException
    {
        internal EmptyArticleListIdException() : base(message: "ArticleList id cannot be empty!")
        {
        }
    }
}
