using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Domain.Events
{
    public record CommentedArticleAdded(User User, CommentedArticle CommentedArticle) : IDomainEvent;
}
