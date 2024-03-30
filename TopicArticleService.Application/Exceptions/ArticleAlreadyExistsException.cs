
namespace TopicArticleService.Application.Exceptions
{
    internal class ArticleAlreadyExistsException : ApplicationException
    {
        internal ArticleAlreadyExistsException(Guid articleId) 
            : base(message: $"Article #{articleId} already exists!")
        {
        }
    }
}
