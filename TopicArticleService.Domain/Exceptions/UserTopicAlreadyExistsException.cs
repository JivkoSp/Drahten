using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Exceptions
{
    public sealed class UserTopicAlreadyExistsException : DomainException
    {
        public UserTopicAlreadyExistsException(Guid userId, Guid topicId) 
            : base(message: $"User #{userId} already subscribed to Topic #{topicId}!")
        {
        }
    }
}
