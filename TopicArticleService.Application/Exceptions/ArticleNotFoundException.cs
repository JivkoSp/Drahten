
namespace TopicArticleService.Application.Exceptions
{
    internal class ArticleNotFoundException : ApplicationException
    {
        internal ArticleNotFoundException(Guid articleId) 
            : base(message: $"Article #{articleId} was NOT found!")
        {
        }
    }
}
