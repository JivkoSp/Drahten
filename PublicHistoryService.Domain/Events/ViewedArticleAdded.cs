﻿using PublicHistoryService.Domain.Entities;
using PublicHistoryService.Domain.ValueObjects;

namespace PublicHistoryService.Domain.Events
{
    public record ViewedArticleAdded(User User, ViewedArticle ViewedArticle) : IDomainEvent;
}
