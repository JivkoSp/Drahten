using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Events
{
    public record UserArticleAdded(Article Article, UserArticle UserArticle) : IDomainEvent;
}
