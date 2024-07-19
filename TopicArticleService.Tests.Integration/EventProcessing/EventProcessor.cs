using System.Text.Json;
using TopicArticleService.Application.Dtos.PrivateHistoryService;
using TopicArticleService.Infrastructure.Dtos;
using TopicArticleService.Tests.Integration.Events;

namespace TopicArticleService.Tests.Integration.EventProcessing
{
    internal enum EventType
    {
        ViewedArticle,
        LikedArticle,
        DislikedArticle,
        CommentedArticle,
        LikedArticleComment,
        DislikedArticleComment,
        TopicSubscription,
        SearchedArticleData,
        Undetermined
    }

    internal class EventProcessor : IEventProcessor
    {
        private EventType DetermineEvent(string notificationMessage)
        {
            var eventType = JsonSerializer.Deserialize<MessageBusEventDto>(notificationMessage);

            switch (eventType.Event)
            {
                case "ViewedArticle":
                    return EventType.ViewedArticle;
                case "LikedArticle":
                    return EventType.LikedArticle;
                case "DislikedArticle":
                    return EventType.DislikedArticle;
                case "CommentedArticle":
                    return EventType.CommentedArticle;
                case "LikedArticleComment":
                    return EventType.LikedArticleComment;
                case "DislikedArticleComment":
                    return EventType.DislikedArticleComment;
                case "TopicSubscription":
                    return EventType.TopicSubscription;
                case "SearchedArticleData":
                    return EventType.SearchedArticleData;
                default:
                    return EventType.Undetermined;
            }
        }

        private void WriteViewedArticle(string message)
        {
            var viewedArticleDto = JsonSerializer.Deserialize<ViewedArticleDto>(message);

            IEventProcessor.Events.Add(new ViewedArticleAdded(viewedArticleDto.ArticleId));
        }

        private void WriteLikedArticle(string message)
        {
            var likedArticleDto = JsonSerializer.Deserialize<LikedArticleDto>(message);

            IEventProcessor.Events.Add(new LikedArticleAdded(likedArticleDto.ArticleId));
        }

        private void WriteDislikedArticle(string message)
        {
            var dislikedArticleDto = JsonSerializer.Deserialize<DislikedArticleDto>(message);

            IEventProcessor.Events.Add(new DislikedArticleAdded(dislikedArticleDto.ArticleId));
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.ViewedArticle:
                    WriteViewedArticle(message);
                    break;
                case EventType.LikedArticle:
                    WriteLikedArticle(message);
                    break;
                case EventType.DislikedArticle:
                    WriteDislikedArticle(message);
                    break;
                case EventType.CommentedArticle:

                    break;
                case EventType.LikedArticleComment:

                    break;
                case EventType.DislikedArticleComment:

                    break;
                case EventType.TopicSubscription:

                    break;
                case EventType.SearchedArticleData:

                    break;
            }
        }
    }
}
