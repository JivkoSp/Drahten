
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class ViewedArticleAlreadyExistsException : DomainException
    {
        public ViewedArticleAlreadyExistsException(Guid articleId, Guid userId, DateTimeOffset dateTime) 
            : base(message: $"Article #{articleId} is already viewed by user: #{userId} on {dateTime}.")
        {
        }
    }
}
