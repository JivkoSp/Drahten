
namespace PrivateHistoryService.Application.Exceptions
{
    public sealed class CommentedArticleNotFoundException : ApplicationException
    {
        internal CommentedArticleNotFoundException(Guid commentedArticleId) 
            : base(message: $"Commented article #{commentedArticleId} was NOT found!")
        {
        }
    }
}
