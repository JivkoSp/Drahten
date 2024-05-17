using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Domain.Events
{
    public record CommentedArticleRemoved(User User, CommentedArticle CommentedArticle) : IDomainEvent;
}
