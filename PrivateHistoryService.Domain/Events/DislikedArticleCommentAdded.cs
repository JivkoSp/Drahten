using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Domain.Events
{
    public record DislikedArticleCommentAdded(User User, DislikedArticleComment DislikedArticleComment) : IDomainEvent;
}
