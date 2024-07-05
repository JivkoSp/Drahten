
namespace PublicHistoryService.Domain.Exceptions
{
    public sealed class SearchedArticleDataAlreadyExistsException : DomainException
    {
        internal SearchedArticleDataAlreadyExistsException(Guid userId, DateTimeOffset dateTime)
            : base(message: $"This article data is already searched by user #{userId} on {dateTime}!")
        {
        }
    }
}
