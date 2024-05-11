using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using TopicArticleService.Application.Dtos.SearchService;
using TopicArticleService.Application.Services.ReadServices;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.Repositories;
using TopicArticleService.Domain.ValueObjects;
using TopicArticleService.Infrastructure.Dtos;
using TopicArticleService.Infrastructure.Extensions;
using TopicArticleService.Infrastructure.SyncDataServices.Grpc;

namespace TopicArticleService.Infrastructure.EventProcessing
{
    internal enum EventType
    {
        DocumentSimilarityCheck,
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
            Console.WriteLine("--> Determining Event Type.");

            var eventType = JsonSerializer.Deserialize<MessageBusEventDto>(notificationMessage);

            switch (eventType.Event)
            {
                case "DocumentSimilarityCheck":
                    Console.WriteLine("--> SearchService event detected!");
                    return EventType.DocumentSimilarityCheck;
                default:
                    Console.WriteLine("--> Could NOT determine the event type!");
                    return EventType.Undetermined;
            }
        }

        private async Task SeedDocument(IAsyncEnumerable<double> similarityScoreResponse, DocumentDto documentDto, 
            IArticleFactory articleFactory, IArticleRepository articleRepository, ITopicReadService topicReadService)
        {
            Console.WriteLine($"\n\n--> Seeding NEW document from SearchService.\n\n");

            await foreach (var similarityScore in similarityScoreResponse)
            {
                //Check if the document has less than 90% similarity with existing documents in SearchService.
                if(similarityScore < 0.9)
                {
                    Console.WriteLine($"\n\n--> Writing a NEW document to the TopicArticleService database.\n\n");

                    var topic = await topicReadService.GetTopicByNameAsync(documentDto.TopicName.ToSnakeCase());

                    var article = articleFactory.Create(Guid.Parse(documentDto.DocumentId), documentDto.PrevTitle, documentDto.Title, 
                        documentDto.Content, documentDto.PublishingDate, documentDto.Author, documentDto.Link, topic.TopicId);

                    await articleRepository.AddArticleAsync(article);
                }
            }
        }

        private async Task CheckDocumentSimilarityAsync(string checkDocumentSimilarityMessage)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var searchServiceDataClient = scope.ServiceProvider.GetRequiredService<ISearchServiceDataClient>();

            var articleFactory = scope.ServiceProvider.GetRequiredService<IArticleFactory>();

            var articleRepository = scope.ServiceProvider.GetRequiredService<IArticleRepository>();

            var topicReadService = scope.ServiceProvider.GetRequiredService<ITopicReadService>();

            var documentDto = JsonSerializer.Deserialize<DocumentDto>(checkDocumentSimilarityMessage);

            var similarityScoreResponse = searchServiceDataClient.DocumentSimilarityCheckAsync(documentDto);

            await SeedDocument(similarityScoreResponse, documentDto, articleFactory, articleRepository, topicReadService);
        }

        public async Task ProcessEventAsync(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.DocumentSimilarityCheck:
                    await CheckDocumentSimilarityAsync(message);
                    break;
            }
        }
    }
}
