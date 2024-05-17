
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class InvalidLikedArticleCommentDateTimeException : DomainException
    {
        internal InvalidLikedArticleCommentDateTimeException() : base(message: "Invalid datetime for liked article comment!")
        {
        }
    }
}
