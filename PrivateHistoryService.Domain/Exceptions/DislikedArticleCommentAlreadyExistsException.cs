
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class DislikedArticleCommentAlreadyExistsException : DomainException
    {
        internal DislikedArticleCommentAlreadyExistsException(Guid articleCommentID, Guid userId) 
            : base(message: $"There is already dislike for article comment #{articleCommentID} from user #{userId}!")
        {
        }
    }
}
