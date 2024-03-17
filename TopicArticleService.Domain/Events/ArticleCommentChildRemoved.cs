using TopicArticleService.Domain.Entities;

namespace TopicArticleService.Domain.Events
{
    public record ArticleCommentChildRemoved(ArticleComment ArticleParentComment, ArticleComment ArticleChildComment) : IDomainEvent;
}
