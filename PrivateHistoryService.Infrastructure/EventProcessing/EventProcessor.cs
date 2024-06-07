using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Application.Services.WriteServices;
using PrivateHistoryService.Domain.ValueObjects;
using PrivateHistoryService.Infrastructure.Dtos;
using System.Text.Json;


namespace PrivateHistoryService.Infrastructure.EventProcessing
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
        Undetermined
    }

    public sealed class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory serviceScopeFactory, IMapper mapper)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("--> PrivateHistoryService EventProcessor: Determining Event Type.");

            var eventType = JsonSerializer.Deserialize<MessageBusEventDto>(notificationMessage);

            switch (eventType.Event)
            {
                case "ViewedArticle":
                    Console.WriteLine("--> PrivateHistoryService EventProcessor: ViewedArticle event detected!");
                    return EventType.ViewedArticle;
                case "LikedArticle":
                    Console.WriteLine("--> PrivateHistoryService EventProcessor: LikedArticle event detected!");
                    return EventType.LikedArticle;
                case "DislikedArticle":
                    Console.WriteLine("--> PrivateHistoryService EventProcessor: DislikedArticle event detected!");
                    return EventType.DislikedArticle;
                case "CommentedArticle":
                    Console.WriteLine("--> PrivateHistoryService EventProcessor: CommentedArticle event detected!");
                    return EventType.CommentedArticle;
                case "LikedArticleComment":
                    Console.WriteLine("--> PrivateHistoryService EventProcessor: LikedArticleComment event detected!");
                    return EventType.LikedArticleComment;
                case "DislikedArticleComment":
                    Console.WriteLine("--> PrivateHistoryService EventProcessor: DislikedArticleComment event detected!");
                    return EventType.DislikedArticleComment;
                case "TopicSubscription":
                    Console.WriteLine("--> PrivateHistoryService EventProcessor: TopicSubscription event detected!");
                    return EventType.TopicSubscription;
                default:
                    return EventType.Undetermined;
            }
        }

        private async Task WriteViewedArticle(string article)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var viewedArticleWriteService = scope.ServiceProvider.GetRequiredService<IViewedArticleWriteService>();
            
            var viewedArticleDto = JsonSerializer.Deserialize<ViewedArticleDto>(article);

            var viewedArticle = _mapper.Map<ViewedArticle>(viewedArticleDto);

            await viewedArticleWriteService.AddViewedArticleAsync(viewedArticle);
        }

        private async Task WriteLikedArticle(string likedArticle)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var likedArticleWriteService = scope.ServiceProvider.GetRequiredService<ILikedArticleWriteService>();

            var likedArticleDto = JsonSerializer.Deserialize<LikedArticleDto>(likedArticle);

            var likedArticleValueObject = _mapper.Map<LikedArticle>(likedArticleDto);

            await likedArticleWriteService.AddLikedArticleAsync(likedArticleValueObject);
        }

        public async Task ProcessEventAsync(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.ViewedArticle:
                    await WriteViewedArticle(message);
                    break;
                case EventType.LikedArticle:
                    await WriteLikedArticle(message);
                    break;
            }
        }
    }
}
