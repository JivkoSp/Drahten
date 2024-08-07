﻿using PublicHistoryService.Domain.Exceptions;

namespace PublicHistoryService.Domain.ValueObjects
{
    public record ViewedArticle
    {
        public ArticleID ArticleID { get; }
        public UserID UserID { get; }
        internal DateTimeOffset DateTime { get; }

        private ViewedArticle() { }

        public ViewedArticle(ArticleID articleId, UserID userId, DateTimeOffset dateTime)
        {
            if (articleId == null)
            {
                throw new NullArticleIdException();
            }

            if (userId == null)
            {
                throw new NullUserIdException();
            }

            if (dateTime == default || dateTime > DateTimeOffset.Now)
            {
                throw new InvalidViewedArticleDateTimeException();
            }

            ArticleID = articleId;
            UserID = userId;
            DateTime = dateTime;
        }
    }
}
