
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class InvalidSearchedTopicDataDateTimeException : DomainException
    {
        internal InvalidSearchedTopicDataDateTimeException() : base(message: "Invalid datetime for searched topic data!")
        {
        }
    }
}
