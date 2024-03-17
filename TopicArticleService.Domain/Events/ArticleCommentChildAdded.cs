using TopicArticleService.Domain.Entities;

namespace TopicArticleService.Domain.Events
{
    public record ArticleCommentChildAdded(ArticleComment ArticleParentComment, ArticleComment ArticleChildComment) : IDomainEvent;
}
