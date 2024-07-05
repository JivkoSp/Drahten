
namespace PublicHistoryService.Domain.Exceptions
{
    public sealed class ViewedArticleNotFoundException : DomainException
    {
        internal ViewedArticleNotFoundException(Guid articleId, Guid userId, DateTimeOffset dateTime)
            : base(message: $"There is no view for article #{articleId} from user #{userId} on {dateTime}!")
        {
        }
    }
}
