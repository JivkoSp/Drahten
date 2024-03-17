
namespace TopicArticleService.Domain.Exceptions
{
    public class EmptyArticleListIdException : DomainException
    {
        public EmptyArticleListIdException() : base(message: "ArticleList id cannot be empty!")
        {
        }
    }
}
