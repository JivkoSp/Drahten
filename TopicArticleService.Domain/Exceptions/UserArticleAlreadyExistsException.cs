
namespace TopicArticleService.Domain.Exceptions
{
    internal class UserArticleAlreadyExistsException : DomainException
    {
        public UserArticleAlreadyExistsException(Guid userId, Guid articleId) 
            : base(message: $"User #{userId} already viewed article #{articleId}!")
        {
        }
    }
}
