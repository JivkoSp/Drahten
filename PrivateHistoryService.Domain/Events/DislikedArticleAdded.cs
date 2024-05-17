using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Domain.Events
{
    public record DislikedArticleAdded(User User, DislikedArticle DislikedArticle) : IDomainEvent;
}
