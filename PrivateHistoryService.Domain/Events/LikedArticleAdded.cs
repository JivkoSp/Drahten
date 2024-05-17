using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Domain.Events
{
    public record LikedArticleAdded(User User, LikedArticle LikedArticle) : IDomainEvent;
}
