using TopicArticleService.Domain.Exceptions;

namespace TopicArticleService.Domain.ValueObjects
{
    public record ArticleCommentDislike
    {
        public UserId UserId { get; }

        public ArticleCommentDislike(UserId userId)
        {
            if (userId == null)
            {
                throw new NullArticleCommentDislikeUserIdException();
            }

            UserId = userId;
        }
    }
}
