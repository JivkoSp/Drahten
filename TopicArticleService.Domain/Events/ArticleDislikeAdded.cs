using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Events
{
    public record ArticleDislikeAdded(Article Article, ArticleDislike Dislike) : IDomainEvent;
}
