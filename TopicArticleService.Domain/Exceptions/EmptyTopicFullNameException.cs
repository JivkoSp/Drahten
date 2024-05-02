
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class EmptyTopicFullNameException : DomainException
    {
        public EmptyTopicFullNameException() : base(message: "Topic full name cannot be empty!")
        {
        }
    }
}
