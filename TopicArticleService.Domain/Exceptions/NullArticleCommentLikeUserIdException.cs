
namespace TopicArticleService.Domain.Exceptions
{
    internal class NullArticleCommentLikeUserIdException : DomainException
    {
        internal NullArticleCommentLikeUserIdException() : base(message: "ArticleCommentLike userId cannot be null!")
        {
        }
    }
}
