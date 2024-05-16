
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class InvalidSearchedTopicDataDateTimeException : DomainException
    {
        public InvalidSearchedTopicDataDateTimeException() : base(message: "Invalid datetime for searched topic data!")
        {
        }
    }
}
