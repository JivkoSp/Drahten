
namespace TopicArticleService.Domain.Exceptions
{
    internal sealed class NullTopicIdException : DomainException
    {
        internal NullTopicIdException() : base(message: "TopicId cannot be null!")
        {
        }
    }
}
