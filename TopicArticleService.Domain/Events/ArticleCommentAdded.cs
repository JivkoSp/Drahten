using TopicArticleService.Domain.Entities;

namespace TopicArticleService.Domain.Events
{
    public record ArticleCommentAdded(Article Article, ArticleComment ArticleComment) : IDomainEvent;
}
