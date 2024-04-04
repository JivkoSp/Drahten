using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Events
{
    public record ArticleDislikeRemoved(Article Article, ArticleDislike Dislike) : IDomainEvent;
}
