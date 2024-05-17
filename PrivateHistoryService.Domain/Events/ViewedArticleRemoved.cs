using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Domain.Events
{
    public record ViewedArticleRemoved(User User, ViewedArticle ViewedArticle) : IDomainEvent;
}
