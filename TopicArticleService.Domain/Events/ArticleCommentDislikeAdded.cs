using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Events
{
    public record ArticleCommentDislikeAdded(ArticleComment ArticleComment, ArticleCommentDislike ArticleCommentDislike) : IDomainEvent;
}
