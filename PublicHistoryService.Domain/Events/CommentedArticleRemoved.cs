using PublicHistoryService.Domain.Entities;
using PublicHistoryService.Domain.ValueObjects;

namespace PublicHistoryService.Domain.Events
{
    public record CommentedArticleRemoved(User User, CommentedArticle CommentedArticle) : IDomainEvent;
}
