using PublicHistoryService.Domain.Entities;
using PublicHistoryService.Domain.ValueObjects;

namespace PublicHistoryService.Domain.Events
{
    public record CommentedArticleAdded(User User, CommentedArticle CommentedArticle) : IDomainEvent;
}
