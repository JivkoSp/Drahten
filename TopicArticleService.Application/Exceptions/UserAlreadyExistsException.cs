
namespace TopicArticleService.Application.Exceptions
{
    internal class UserAlreadyExistsException : ApplicationException
    {
        internal UserAlreadyExistsException(Guid userId) 
            : base(message: $"User #{userId} already exists!")
        {
        }
    }
}
