
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class LikedArticleCommentAlreadyExistsException : DomainException
    {
        internal LikedArticleCommentAlreadyExistsException(Guid articleCommentID, Guid userId) 
            : base(message: $"There is already like for article comment #{articleCommentID} from user #{userId}!")
        {
        }
    }
}
