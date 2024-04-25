using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Application.Services.ReadServices;
using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.Repositories;
using TopicArticleService.Infrastructure.Dtos;
using TopicArticleService.Infrastructure.EntityFramework.Models;

namespace TopicArticleService.Infrastructure.EventProcessing
{
    internal enum EventType
    {
        User_Published,
        Undetermined
    }

    public sealed class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;

        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("--> Determining Event Type.");

            var eventType = JsonSerializer.Deserialize<MessageBusEventDto>(notificationMessage);

            switch (eventType.Event)
            {
                case "User_Published":
                    Console.WriteLine("--> UserService event detected!");
                    return EventType.User_Published;
                default:
                    Console.WriteLine("--> Could NOT determine the event type!");
                    return EventType.Undetermined;
            }
        }
        
        private async Task AddUserAsync(string userPublishedMessage)
        {
            using var score = _serviceScopeFactory.CreateScope();

            var userReadService = score.ServiceProvider.GetRequiredService<IUserReadService>();

            var userRepository = score.ServiceProvider.GetRequiredService<IUserRepository>();

            var userDto = JsonSerializer.Deserialize<UserDto>(userPublishedMessage);

            var user = _mapper.Map<User>(userDto);

            if (await userReadService.ExistsByIdAsync(user.Id) == false)
            {
                await userRepository.AddUserAsync(user);

                Console.WriteLine($"--> UserService NEW USER: {user.Id} added!");
            }
        }

        public EventProcessor(IServiceScopeFactory serviceScopeFactory, IMapper mapper)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }

        public async Task ProcessEventAsync(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.User_Published:
                    await AddUserAsync(message);
                    break;
            }
        }
    }
}
