﻿using PublicHistoryService.Domain.Exceptions;

namespace PublicHistoryService.Domain.ValueObjects
{
    public record SearchedArticleData
    {
        public ArticleID ArticleID { get; }
        public UserID UserID { get; }
        internal SearchedData SearchedData { get; }
        internal DateTimeOffset DateTime { get; }

        private SearchedArticleData() { }

        public SearchedArticleData(ArticleID articleId, UserID userId, SearchedData searchedData, DateTimeOffset dateTime)
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

            if (dateTime == default || dateTime > DateTimeOffset.Now)
            {
                throw new InvalidSearchedArticleDataDateTimeException();
            }

            ArticleID = articleId;
            UserID = userId;
            SearchedData = searchedData;
            DateTime = dateTime;
        }
    }
}
