using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Domain.Events
{
    public record ViewedArticleAdded(User User, ViewedArticle ViewedArticle) : IDomainEvent;
}
