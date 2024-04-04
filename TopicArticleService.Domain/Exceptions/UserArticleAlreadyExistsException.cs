
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class UserArticleAlreadyExistsException : DomainException
    {
        internal UserArticleAlreadyExistsException(Guid userId, Guid articleId) 
            : base(message: $"User #{userId} already viewed article #{articleId}!")
        {
        }
    }
}
