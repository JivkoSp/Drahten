using TopicArticleService.Domain.Exceptions;

namespace TopicArticleService.Domain.ValueObjects
{
    public record ArticleCommentLike
    {
        public UserId UserId { get; }

        public ArticleCommentLike(UserId userId)
        {
            if (userId == null)
            {
                throw new NullArticleCommentLikeUserIdException();
            }

            UserId = userId;
        }
    }
}
