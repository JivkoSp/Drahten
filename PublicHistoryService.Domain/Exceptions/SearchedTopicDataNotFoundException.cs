
namespace PublicHistoryService.Domain.Exceptions
{
    public sealed class SearchedTopicDataNotFoundException : DomainException
    {
        internal SearchedTopicDataNotFoundException(Guid topicId, Guid userId, DateTimeOffset dateTime)
            : base(message: $"There is no searched data for topic #{topicId} from user #{userId} on {dateTime}!")
        {
        }
    }
}
