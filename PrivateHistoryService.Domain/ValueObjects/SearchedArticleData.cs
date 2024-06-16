using PrivateHistoryService.Domain.Exceptions;

namespace PrivateHistoryService.Domain.ValueObjects
{
    public record SearchedArticleData
    {
        public ArticleID ArticleID { get; }
        public UserID UserID { get; }
        internal SearchedData SearchedData { get; }
        internal SearchedDataAnswer SearchedDataAnswer { get; }
        internal SearchedDataAnswerContext SearchedDataAnswerContext { get; }
        internal DateTimeOffset DateTime { get; }

        private SearchedArticleData() {}

        public SearchedArticleData(ArticleID articleId, UserID userId, SearchedData searchedData, 
            SearchedDataAnswer searchedDataAnswer, SearchedDataAnswerContext searchedDataAnswerContext, DateTimeOffset dateTime)
        {
            if (articleId == null)
            {
                throw new NullArticleIdException();
            }

            if (userId == null)
            {
                throw new NullUserIdException();
            }

            if (searchedData == null)
            {
                throw new NullSearchedDataException();
            }

            if (searchedDataAnswer == null)
            {
                throw new NullSearchedDataAnswerException();
            }

            if (searchedDataAnswerContext == null)
            {
                throw new NullSearchedDataAnswerContextException();
            }
            
            if (dateTime == default || dateTime > DateTimeOffset.Now)
            {
                throw new InvalidSearchedArticleDataDateTimeException();
            }

            ArticleID = articleId;
            UserID = userId;
            SearchedData = searchedData;
            SearchedDataAnswer = searchedDataAnswer;
            SearchedDataAnswerContext = searchedDataAnswerContext;
            DateTime = dateTime;
        }
    }
}
