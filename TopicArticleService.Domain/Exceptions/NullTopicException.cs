
namespace TopicArticleService.Domain.Exceptions
{
    internal class NullTopicException : DomainException
    {
        internal NullTopicException() : base(message: "Topic cannot be null!")
        {
        }
    }
}
