
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class LikedArticleAlreadyExistsException : DomainException
    {
        internal LikedArticleAlreadyExistsException(Guid articleId, Guid userId) 
            : base(message: $"There is already like for article #{articleId} from user #{userId}!")
        {
        }
    }
}
