
namespace TopicArticleService.Domain.Exceptions
{
    public sealed class NullArticleCommentLikeUserIdException : DomainException
    {
        internal NullArticleCommentLikeUserIdException() : base(message: "ArticleCommentLike userId cannot be null!")
        {
        }
    }
}
