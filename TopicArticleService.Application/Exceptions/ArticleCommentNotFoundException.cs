
namespace TopicArticleService.Application.Exceptions
{
    internal class ArticleCommentNotFoundException : ApplicationException
    {
        internal ArticleCommentNotFoundException(Guid articleCommentId) 
            : base(message: $"ArticleComment #{articleCommentId} was NOT found!")
        {
        }
    }
}
