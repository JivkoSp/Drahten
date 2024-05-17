
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class DislikedArticleAlreadyExistsException : DomainException
    {
        internal DislikedArticleAlreadyExistsException(Guid articleId, Guid userId) 
            : base(message: $"There is already dislike for article #{articleId} from user #{userId}!")
        {
        }
    }
}
