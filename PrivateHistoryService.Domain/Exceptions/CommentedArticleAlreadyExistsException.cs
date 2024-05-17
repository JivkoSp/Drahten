
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class CommentedArticleAlreadyExistsException : DomainException
    {
        internal CommentedArticleAlreadyExistsException(Guid userId, DateTimeOffset dateTime) 
            : base(message: $"There is already the same article comment made by user #{userId} on {dateTime}!")
        {
        }
    }
}
