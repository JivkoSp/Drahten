
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class InvalidLikedArticleCommentDateTimeException : DomainException
    {
        public InvalidLikedArticleCommentDateTimeException() : base(message: "Invalid datetime for liked article comment!")
        {
        }
    }
}
