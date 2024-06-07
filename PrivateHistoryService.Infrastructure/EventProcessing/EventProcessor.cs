﻿using AutoMapper;
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

        public async Task ProcessEventAsync(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.ViewedArticle:
                    await WriteViewedArticle(message);
                    break;
            }
        }
    }
}