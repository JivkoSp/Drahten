using TopicArticleService.Domain.Entities;

namespace TopicArticleService.Domain.Events
{
    public record TopicAdded(Article Article, Topic Topic) : IDomainEvent;
}
