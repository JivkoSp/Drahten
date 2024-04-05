using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Events
{
    public record ArticleCommentLikeRemoved(ArticleComment ArticleComment, ArticleCommentLike ArticleCommentLike) : IDomainEvent;
}
