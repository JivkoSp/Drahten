using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Events
{
    public record ArticleLikeRemoved(Article Article, ArticleLike Like) : IDomainEvent;
}
