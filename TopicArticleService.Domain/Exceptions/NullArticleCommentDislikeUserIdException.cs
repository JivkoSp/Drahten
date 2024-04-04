
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class NullArticleCommentDislikeUserIdException : DomainException
    {
        internal NullArticleCommentDislikeUserIdException() : base(message: "ArticleCommentDislike userId cannot be null!")
        {
        }
    }
}
