using PublicHistoryService.Domain.Entities;
using PublicHistoryService.Domain.ValueObjects;

namespace PublicHistoryService.Domain.Events
{
    public record SearchedArticleDataAdded(User User, SearchedArticleData SearchedArticleData) : IDomainEvent;
}
