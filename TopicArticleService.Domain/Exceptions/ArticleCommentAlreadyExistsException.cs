
namespace TopicArticleService.Domain.Exceptions
{
    internal class ArticleCommentAlreadyExistsException : DomainException
    {
        internal ArticleCommentAlreadyExistsException(Guid articleId, Guid userId) 
            : base(message: $"Article #{articleId} already has comment from user #{userId}!")
        {
        }
    }
}
