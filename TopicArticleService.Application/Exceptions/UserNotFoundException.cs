
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Application.Exceptions
{
    internal class UserNotFoundException : ApplicationException
    {
        public UserNotFoundException(Guid userId) 
            : base(message: $"User #{userId} was NOT found!")
        {
        }
    }
}
