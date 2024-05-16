
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class InvalidSubscribedTopicDateTimeException : DomainException
    {
        public InvalidSubscribedTopicDateTimeException() : base(message: "Invalid datetime for subscribed topic!")
        {
        }
    }
}
