﻿
namespace PublicHistoryService.Domain.Exceptions
{
    public sealed class CommentedArticleNotFoundException : DomainException
    {
        internal CommentedArticleNotFoundException(Guid articleId, Guid userId, string articleComment, DateTimeOffset dateTime)
            : base(message: $"There is no article comment: '{articleComment}' for article #{articleId}, made from user #{userId} on {dateTime}!")
        {
        }
    }
}
