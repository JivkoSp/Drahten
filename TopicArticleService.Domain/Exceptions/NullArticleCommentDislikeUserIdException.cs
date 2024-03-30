
namespace TopicArticleService.Domain.Exceptions
{
    internal class NullArticleCommentDislikeUserIdException : DomainException
    {
        internal NullArticleCommentDislikeUserIdException() : base(message: "ArticleCommentDislike userId cannot be null!")
        {
        }
    }
}
