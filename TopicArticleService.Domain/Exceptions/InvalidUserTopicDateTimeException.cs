
namespace TopicArticleService.Domain.Exceptions
{
    internal sealed class InvalidUserTopicDateTimeException : DomainException
    {
        internal InvalidUserTopicDateTimeException() : base(message: "Invalid UserTopic datetime!")
        {
        }
    }
}
