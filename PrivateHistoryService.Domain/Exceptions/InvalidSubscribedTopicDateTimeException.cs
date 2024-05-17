
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class InvalidSubscribedTopicDateTimeException : DomainException
    {
        internal InvalidSubscribedTopicDateTimeException() : base(message: "Invalid datetime for subscribed topic!")
        {
        }
    }
}
