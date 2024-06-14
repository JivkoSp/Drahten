using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Application.Services.ReadServices;
using PrivateHistoryService.Application.Services.WriteServices;
using PrivateHistoryService.Domain.Factories.Interfaces;
using PrivateHistoryService.Domain.Repositories;
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
        SearchedArticleData,
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
                    Console.WriteLine("PrivateHistoryService --> EventProcessor: ViewedArticle event detected!");
                    return EventType.ViewedArticle;
                case "LikedArticle":
                    Console.WriteLine("PrivateHistoryService --> EventProcessor: LikedArticle event detected!");
                    return EventType.LikedArticle;
                case "DislikedArticle":
                    Console.WriteLine("PrivateHistoryService --> EventProcessor: DislikedArticle event detected!");
                    return EventType.DislikedArticle;
                case "CommentedArticle":
                    Console.WriteLine("PrivateHistoryService --> EventProcessor: CommentedArticle event detected!");
                    return EventType.CommentedArticle;
                case "LikedArticleComment":
                    Console.WriteLine("PrivateHistoryService --> EventProcessor: LikedArticleComment event detected!");
                    return EventType.LikedArticleComment;
                case "DislikedArticleComment":
                    Console.WriteLine("PrivateHistoryService --> EventProcessor: DislikedArticleComment event detected!");
                    return EventType.DislikedArticleComment;
                case "TopicSubscription":
                    Console.WriteLine("PrivateHistoryService --> EventProcessor: TopicSubscription event detected!");
                    return EventType.TopicSubscription;
                case "SearchedArticleData":
                    Console.WriteLine("PrivateHistoryService --> EventProcessor: SearchedArticleData event detected!");
                    return EventType.SearchedArticleData;
                default:
                    return EventType.Undetermined;
            }
        }

        private async Task WriteViewedArticle(string article)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var viewedArticleWriteService = scope.ServiceProvider.GetRequiredService<IViewedArticleWriteService>();

            var userReadService = scope.ServiceProvider.GetRequiredService<IUserReadService>();

            var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

            var userFactory = scope.ServiceProvider.GetRequiredService<IUserFactory>();

            var viewedArticleDto = JsonSerializer.Deserialize<ViewedArticleDto>(article);

            var viewedArticle = _mapper.Map<ViewedArticle>(viewedArticleDto);

            var alreadyExists = await userReadService.ExistsByIdAsync(viewedArticle.UserID);

            if (alreadyExists is false)
            {
                var user = userFactory.Create(viewedArticle.UserID);

                await userRepository.AddUserAsync(user);
            }

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

        private async Task WriteDislikedArticle(string dislikedArticle)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var dislikedArticleWriteService = scope.ServiceProvider.GetRequiredService<IDislikedArticleWriteService>();

            var dislikedArticleDto = JsonSerializer.Deserialize<DislikedArticleDto>(dislikedArticle);

            var dislikedArticleValueObject = _mapper.Map<DislikedArticle>(dislikedArticleDto);

            await dislikedArticleWriteService.AddDislikedArticleAsync(dislikedArticleValueObject);
        }

        private async Task WriteCommentedArticle(string commentedArticle)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var commentedArticleWriteService = scope.ServiceProvider.GetRequiredService<ICommentedArticleWriteService>();

            var commentedArticleDto = JsonSerializer.Deserialize<CommentedArticleDto>(commentedArticle);

            var commentedArticleValueObject = _mapper.Map<CommentedArticle>(commentedArticleDto);

            await commentedArticleWriteService.AddCommentedArticleAsync(commentedArticleValueObject);
        }

        private async Task WriteLikedArticleComment(string likedArticleComment)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var likedArticleCommentWriteService = scope.ServiceProvider.GetRequiredService<ILikedArticleCommentWriteService>();

            var likedArticleCommentDto = JsonSerializer.Deserialize<LikedArticleCommentDto>(likedArticleComment);

            var likedArticleCommentValueObject = _mapper.Map<LikedArticleComment>(likedArticleCommentDto);

            await likedArticleCommentWriteService.AddLikedArticleCommentAsync(likedArticleCommentValueObject);
        }

        private async Task WriteDislikedArticleComment(string dislikedArticleComment)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var dislikedArticleCommentWriteService = scope.ServiceProvider.GetRequiredService<IDislikedArticleCommentWriteService>();

            var dislikedArticleCommentDto = JsonSerializer.Deserialize<DislikedArticleCommentDto>(dislikedArticleComment);

            var dislikedArticleCommentValueObject = _mapper.Map<DislikedArticleComment>(dislikedArticleCommentDto);

            await dislikedArticleCommentWriteService.AddDislikedArticleCommentAsync(dislikedArticleCommentValueObject);
        }

        private async Task WriteTopicSubscription(string topicSubscription)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var topicSubscriptionWriteService = scope.ServiceProvider.GetRequiredService<ITopicSubscriptionWriteService>();

            var topicSubscriptionDto = JsonSerializer.Deserialize<TopicSubscriptionDto>(topicSubscription);

            var topicSubscriptionValueObject = _mapper.Map<TopicSubscription>(topicSubscriptionDto);

            await topicSubscriptionWriteService.AddTopicSubscriptionAsync(topicSubscriptionValueObject);
        }

        private async Task WriteSearchedArticleData(string searchedArticleData)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var searchedArticleDataWriteService = scope.ServiceProvider.GetRequiredService<ISearchedArticleDataWriteService>();

            var searchedArticleDataDto = JsonSerializer.Deserialize<SearchedArticleDataDto>(searchedArticleData);

            var searchedArticleDataValueObject = _mapper.Map<SearchedArticleData>(searchedArticleDataDto);

            await searchedArticleDataWriteService.AddSearchedArticleDataAsync(searchedArticleDataValueObject);
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
                case EventType.DislikedArticle:
                    await WriteDislikedArticle(message);
                    break;
                case EventType.CommentedArticle:
                    await WriteCommentedArticle(message);
                    break;
                case EventType.LikedArticleComment:
                    await WriteLikedArticleComment(message);
                    break;
                case EventType.DislikedArticleComment:
                    await WriteDislikedArticleComment(message);
                    break;
                case EventType.TopicSubscription:
                    await WriteTopicSubscription(message);
                    break;
                case EventType.SearchedArticleData:
                    await WriteSearchedArticleData(message);
                    break;
            }
        }
    }
}
