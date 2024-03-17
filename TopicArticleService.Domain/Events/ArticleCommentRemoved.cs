using TopicArticleService.Domain.Entities;

namespace TopicArticleService.Domain.Events
{
    public record ArticleCommentRemoved(Article Article, ArticleComment ArticleComment) : IDomainEvent;
}
