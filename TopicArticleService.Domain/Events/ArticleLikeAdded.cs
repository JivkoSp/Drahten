using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Events
{
    public record ArticleLikeAdded(Article Article, ArticleLike Like) : IDomainEvent;
}
