using PublicHistoryService.Domain.Entities;
using PublicHistoryService.Domain.ValueObjects;

namespace PublicHistoryService.Domain.Events
{
    public record ViewedArticleRemoved(User User, ViewedArticle ViewedArticle) : IDomainEvent;
}
