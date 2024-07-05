
namespace PublicHistoryService.Domain.Exceptions
{
    public sealed class ViewedArticleAlreadyExistsException : DomainException
    {
        internal ViewedArticleAlreadyExistsException(Guid articleId, Guid userId, DateTimeOffset dateTime)
            : base(message: $"Article #{articleId} is already viewed by user: #{userId} on {dateTime}.")
        {
        }
    }
}
