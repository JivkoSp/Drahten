using TopicArticleService.Domain.Exceptions;

namespace TopicArticleService.Domain.ValueObjects
{
    public record ArticleLike
    {
        public ArticleID ArticleID { get; }
        public UserID UserID { get; }
        public DateTime DateTime { get; }
    
        private ArticleLike()
        {
        }

        public ArticleLike(ArticleID articleId, UserID userId, string dateTimeString)
        {
            if(articleId == null)
            {
                throw new NullArticleLikeArticleIdException();
            }

            if (userId == null)
            {
                throw new NullArticleLikeUserIdException();
            }

            if (!DateTime.TryParse(dateTimeString, out DateTime dateTime) || dateTime == default || dateTime > DateTime.Now)
            {
                throw new InvalidArticleLikeDateTimeException();
            }

            ArticleID = articleId;
            UserID = userId;
            DateTime = dateTime;
        }

        //Conversion from ValueObject to String.
        public override string ToString()
            => $"{ArticleID}, {UserID}, {DateTime}";

        //Conversion from String to ValueObject.
        public static ArticleLike Create(string value)
        {
            var splitArticleLike = value.Split(',');
            return new ArticleLike(Guid.Parse(splitArticleLike[0]), Guid.Parse(splitArticleLike[1]), splitArticleLike[2]);
        }
    }
}
