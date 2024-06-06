﻿using TopicArticleService.Application.Dtos.PrivateHistoryService;

namespace TopicArticleService.Application.AsyncDataServices
{
    public interface IMessageBusPublisher
    {
        void PublishViewedArticle(ViewedArticleDto viewedArticleDto);
    }
}
