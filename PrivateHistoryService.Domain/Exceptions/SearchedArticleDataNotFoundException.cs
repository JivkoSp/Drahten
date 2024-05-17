
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class SearchedArticleDataNotFoundException : DomainException
    {
        internal SearchedArticleDataNotFoundException(Guid articleId, Guid userId, DateTimeOffset dateTime) 
            : base(message: $"There is no searched data for article #{articleId} from user #{userId} on {dateTime}!")
        {
        }
    }
}
