
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class NullTopicException : DomainException
    {
        internal NullTopicException() : base(message: "Topic cannot be null!")
        {
        }
    }
}
