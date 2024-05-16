
namespace PrivateHistoryService.Domain.Exceptions
{
    public sealed class InvalidDislikedArticleCommentDateTimeException : DomainException
    {
        public InvalidDislikedArticleCommentDateTimeException() : base(message: "Invalid datetime for disliked article comment!")
        {
        }
    }
}
