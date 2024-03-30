using TopicArticleService.Domain.Entities;

namespace TopicArticleService.Domain.Events
{
    public record TopicChildAdded(Topic ParentTopic, Topic ChildTopic) : IDomainEvent;
}
