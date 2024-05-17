
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class InvalidDislikedArticleCommentDateTimeException : DomainException
    {
        internal InvalidDislikedArticleCommentDateTimeException() : base(message: "Invalid datetime for disliked article comment!")
        {
        }
    }
}
