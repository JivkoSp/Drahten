
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class SearchedTopicDataAlreadyExistsException : DomainException
    {
        internal SearchedTopicDataAlreadyExistsException(Guid userId, DateTimeOffset dateTime) 
            : base(message: $"This topic data is already searched by user #{userId} on {dateTime}!")
        {
        }
    }
}
