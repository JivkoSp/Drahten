using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using TopicArticleService.Application.Dtos.SearchService;
using TopicArticleService.Application.Services.ReadServices;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.Repositories;
using TopicArticleService.Infrastructure.AsyncDataServices;
using TopicArticleService.Infrastructure.Dtos;
using TopicArticleService.Infrastructure.Extensions;
using TopicArticleService.Infrastructure.SyncDataServices.Grpc;

namespace TopicArticleService.Infrastructure.EventProcessing
{
    internal enum EventType
    {
        DocumentSimilarityCheck,
        NewDocument,
        Undetermined
    }

    public sealed class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMessageBusPublisher _messageBusPublisher;
        private readonly IMapper _mapper;
        private readonly ILogger<EventProcessor> _logger;

        public EventProcessor(IServiceScopeFactory serviceScopeFactory, IMessageBusPublisher messageBusPublisher,
            IMapper mapper, ILogger<EventProcessor> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _messageBusPublisher = messageBusPublisher;
            _mapper = mapper;
            _logger = logger;
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            _logger.LogInformation("--> Determining Event Type.");

            var eventType = JsonSerializer.Deserialize<MessageBusEventDto>(notificationMessage);

            switch (eventType.Event)
            {
                case "DocumentSimilarityCheck":
                    _logger.LogInformation("--> SearchService event detected!");
                    return EventType.DocumentSimilarityCheck;
                case "NewDocument":
                    _logger.LogInformation("--> SearchService event detected!");
                    return EventType.NewDocument;
                default:
                    _logger.LogInformation("--> Could NOT determine the event type!");
                    return EventType.Undetermined;
            }
        }

        private async Task SeedDocument(DocumentDto documentDto, IArticleFactory articleFactory, 
            IArticleRepository articleRepository, ITopicReadService topicReadService)
        {
            Console.WriteLine($"\n\n--> Seeding NEW document from SearchService.\n\n");

            _logger.LogInformation("--> Seeding NEW document from SearchService.");

            var topic = await topicReadService.GetTopicByNameAsync(documentDto.TopicName.ToSnakeCase());

            var article = articleFactory.Create(Guid.Parse(documentDto.DocumentId), documentDto.PrevTitle, documentDto.Title,
                documentDto.Content, documentDto.PublishingDate, documentDto.Author, documentDto.Link, topic.TopicId);

            await articleRepository.AddArticleAsync(article);
        }

        private void CheckDocumentSimilarity(string checkDocumentSimilarityMessage)
        {
            var documentDto = JsonSerializer.Deserialize<DocumentDto>(checkDocumentSimilarityMessage);

            _messageBusPublisher.PublishNewDocument(documentDto);
        }

        private async Task WriteDocument(string document)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var articleFactory = scope.ServiceProvider.GetRequiredService<IArticleFactory>();

            var articleRepository = scope.ServiceProvider.GetRequiredService<IArticleRepository>();

            var topicReadService = scope.ServiceProvider.GetRequiredService<ITopicReadService>();

            var documentDto = JsonSerializer.Deserialize<DocumentDto>(document);

            await SeedDocument(documentDto, articleFactory, articleRepository, topicReadService);
        }

        public async Task ProcessEventAsync(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.DocumentSimilarityCheck:
                    CheckDocumentSimilarity(message);
                    break;
                case EventType.NewDocument:
                    await WriteDocument(message);
                    break;
            }
        }
    }
}
