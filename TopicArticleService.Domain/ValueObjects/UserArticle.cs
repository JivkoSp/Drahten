using TopicArticleService.Domain.Exceptions;

namespace TopicArticleService.Domain.ValueObjects
{
    public record UserArticle
    {
        public UserID UserID { get; }
        public ArticleID ArticleId { get; }

        private UserArticle()
        {
        }

        public UserArticle(UserID userID, ArticleID articleId)
        {
            if(userID == null)
            {
                throw new NullUserIdException();
            }

            if(articleId == null)
            {
                throw new NullArticleIdException();
            }

            UserID = userID;

            ArticleId = articleId;
        }
    }
}
